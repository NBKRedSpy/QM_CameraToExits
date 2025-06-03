using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CameraToExits
{
    public class CameraExitService
    {
        public void MoveCameraToDown(State state)
        {
            MapObstacles mapObstacles = state.Get<MapObstacles>();

            //Only down elevators
            InteractElevator elevator = mapObstacles.Elevators.SingleOrDefault(x => x.IsElevatorGoesDown());

            if (elevator == null) return;

            MoveCamera(elevator.MapObstacle, state.Get<GameCamera>());
        }

        public void MoveCameraToUp(State state)
        {
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

            //There should always be an up path, but just in case.\
            if (mapObstacle == null) return;        

            MoveCamera(mapObstacle, state.Get<GameCamera>());
        }

        /// <summary>
        /// Moves the camera to the obstacle if the location has been explored. 
        /// </summary>
        /// <param name="obstacle"></param>
        private void MoveCamera(MapObstacle obstacle, GameCamera camera)
        {
            if (obstacle._mapGrid.GetCell(obstacle.Position).isExplored)
            {
                camera.SetCameraMode(CameraMode.BorderMove);

                camera.MoveCameraToPosition(new Vector3(obstacle.Renderer.transform.position.x,
                    obstacle.Renderer.transform.position.y), .25f);
            }
        }

    }
}
