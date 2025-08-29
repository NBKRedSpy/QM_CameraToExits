using ModConfigMenu;
using ModConfigMenu.Objects;
using System.Collections.Generic;

namespace CameraToExits.Mcm
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config, Logger logger) : base (config, logger) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Camera To Exits", new List<ConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.CycleMilliseconds),
                    "If the cycle button is pressed under this amount of time (in milliseconds), it will move to the next up/down target.  For example, if there is a drop pod and an exit, the 'Move Down' key will cycle between those two if the key is pressed again within this amount of time.", 1, 10000, "Cycle time"),

                CreateReadOnly(nameof(ModConfig.MoveToDownElevatorKey)),
                CreateReadOnly(nameof(ModConfig.MoveToUpElevatorKey)),
            }, OnSave);
        }
    }
}
