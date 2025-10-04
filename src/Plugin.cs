using CameraToExits.Mcm;
using CameraToExits_Bootstrap;
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
    public class Plugin : BootstrapMod
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Logger Logger = new Logger();

        public static CameraExitService CameraExitService { get; private set; } = new CameraExitService();

        private static McmConfiguration McmConfiguration;

        public Plugin(HookEvents hookEvents, bool isBeta) : base(hookEvents, isBeta)
        {
            hookEvents.AfterConfigsLoaded += AfterConfig;
            hookEvents.DungeonUpdateAfterGameLoop += DungeonUpdateAfterGameLoop;
        }

        public static void AfterConfig(IModContext context)
        {
            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath);

            McmConfiguration = new McmConfiguration(Config, Plugin.Logger);
            McmConfiguration.TryConfigure();

        }

        public static void DungeonUpdateAfterGameLoop(IModContext context)
        {
            if (UI.IsAnyShowing(typeof(DungeonHudScreen))) return;

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
