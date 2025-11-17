using MGSC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace CameraToExits
{
    public class CameraExitService
    {

        /// <summary>
        /// The location of the last "down" item that was selected.
        /// </summary>
        /// <remarks>The player travels down to new levels.</remarks>
        private CellPosition LastDownObstaclePosition { get; set; } = CellPosition.Zero;


        /// <summary>
        /// The last time that the down elevator or drop pod was cycled.
        /// Used to determine if the user is cycling or the hotkey should start over
        /// with the first entry.
        /// </summary>
        private Stopwatch LastCycleTime { get; set; }


        public CameraExitService()
        {
            LastCycleTime = new Stopwatch();
            LastCycleTime.Start();
        }

        public void MoveCameraToDown(State state)
        {
            GameCamera camera = state.Get<GameCamera>();
            MapObstacles mapObstacles = state.Get<MapObstacles>();
            List<MapObstacle> cameraTargets = new List<MapObstacle>();

            //Down elevators
            cameraTargets.AddRange(mapObstacles.Elevators
                .Where(x => x.IsElevatorGoesDown() && IsExplored(x.MapObstacle))
                .Select(x => x.MapObstacle)
                );

            //Drop pod

            //This is how the base game indicates a drop pod
            //  obstacle.Store != null && (obstacle.Store is AutonomousCapsuleStore || obstacle.HasComponent<AirdropCapsuleCnA>()
            //Should only be one, but just in case.
            //  Intentionally not checking for explored since it shows on the minimap anyway.
            cameraTargets.AddRange(
                mapObstacles.Obstacles.Where(obstacle => obstacle.Store != null && 
                    (obstacle.Store is AutonomousCapsuleStore || obstacle.HasComponent<AirdropCapsuleCnA>())
                ));

            //The obstacle that will be moved to.
            MapObstacle cameraTarget;

            if (cameraTargets.Count == 0)
            {
                ResetLastDownCycle();
                return;
            }
            //------Get the item to move to.
            //Only one item or user is trying to cycle
            else if (cameraTargets.Count == 1 || LastCycleTime.ElapsedMilliseconds > Plugin.Config.CycleMilliseconds)
            {
                cameraTarget = cameraTargets[0];
            }
            else
            {
                //Move to the next item.
                //I don't think there will ever be more than two, but just in case.
                int lastTargetIndex = cameraTargets.IndexOf(
                    cameraTargets.FirstOrDefault(x => x.Position.Equals(LastDownObstaclePosition))
                    );

                int nextIndex = (lastTargetIndex + 1) % cameraTargets.Count;
                cameraTarget = cameraTargets[nextIndex];
            }

            SetLastDownCycle(cameraTarget.Position);
            MoveCamera(cameraTarget, camera);
        }

        private void ResetLastDownCycle()
        {
            SetLastDownCycle(CellPosition.Zero);
        }

        private void SetLastDownCycle(CellPosition position)
        {
            LastDownObstaclePosition = position;
            LastCycleTime.Restart();
        }

        public void MoveCameraToUp(State state)
        {
            //Always reset
            ResetLastDownCycle();

            //Elevator up or Shuttle
            MapObstacles mapObstacles = state.Get<MapObstacles>();

            MapObstacle mapObstacle = null;

            //Only up elevators
            mapObstacle = mapObstacles.Elevators.SingleOrDefault(x => !x.IsElevatorGoesDown())?.MapObstacle;

            if (mapObstacle == null)
            {
                //try for shuttle
                mapObstacle = mapObstacles.Obstacles.SingleOrDefault(x => x.GetComponent<Shuttle>() != null);
            }

            //Nothing explored.
            if (mapObstacle == null) return;        

            MoveCamera(mapObstacle, state.Get<GameCamera>());
        }


        private bool IsExplored(MapObstacle obstacle)
        {
            return (obstacle._mapGrid.GetCell(obstacle.Position).IsExplored);
        }

        /// <summary>
        /// Moves the camera to the obstacle
        /// </summary>
        /// <param name="obstacle"></param>
        private void MoveCamera(MapObstacle obstacle, GameCamera camera)
        {
            camera.SetCameraMode(CameraMode.BorderMove);

            camera.MoveCameraToPosition(new Vector3(obstacle.Renderer.transform.position.x,
                obstacle.Renderer.transform.position.y), .25f);
        }

    }
}
