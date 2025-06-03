using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CameraToExits
{
    public static class Plugin
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Logger Logger = new Logger();

        public static CameraExitService CameraExitService { get; private set; } = new CameraExitService();

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {

            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath);
        }

        [Hook(ModHookType.DungeonUpdateAfterGameLoop)]
        public static void DungeonUpdateAfterGameLoop(IModContext context)
        {
            if (InputHelper.GetKeyDown(Config.MoveToDownElevatorKey))
            {
                CameraExitService.MoveCameraToDown(context.State);
            }
            else if (InputHelper.GetKeyDown(Config.MoveToUpElevatorKey))
            {
                CameraExitService.MoveCameraToUp(context.State);
            }
        }

    }
}
