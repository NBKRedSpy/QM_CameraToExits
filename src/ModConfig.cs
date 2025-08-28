using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraToExits.Mcm;
using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace CameraToExits
{
    public class ModConfig : IMcmConfigTarget
    {

        /// <summary>
        /// The minimum amount of time that the user has to press the same key
        /// to cycle between targets.
        /// </summary>
        public int CycleMilliseconds { get; set; } = 1000;

        /// <summary>
        /// When pressed, moves to the elevator that goes down.
        /// </summary>
        public KeyCode MoveToDownElevatorKey { get; set; } = KeyCode.LeftBracket;

        /// <summary>
        /// When pressed, moves to the elevator that goes up or the shuttle.
        /// </summary>
        public KeyCode MoveToUpElevatorKey { get; set; } = KeyCode.RightBracket;


        private static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };


        public static ModConfig LoadConfig(string configPath)
        {
            ModConfig config;

            SerializerSettings.Converters.Add(new StringEnumConverter()
            {
                AllowIntegerValues = true,
            });

            if (File.Exists(configPath))
            {
                try
                {
                    string sourceJson = File.ReadAllText(configPath);

                    config = JsonConvert.DeserializeObject<ModConfig>(sourceJson, SerializerSettings);

                    //Add any new elements that have been added since the last mod version the user had.
                    string upgradeConfig = JsonConvert.SerializeObject(config, SerializerSettings);

                    if (upgradeConfig != sourceJson)
                    {
                        Plugin.Logger.Log("Updating config with missing elements");
                        //re-write
                        File.WriteAllText(configPath, upgradeConfig);
                    }


                    return config;
                }
                catch (Exception ex)
                {
                    Plugin.Logger.LogError(ex,"Error parsing configuration.  Ignoring config file and using defaults");

                    //Not overwriting in case the user just made a typo.
                    config = new ModConfig();
                    return config;
                }
            }
            else
            {
                config = new ModConfig();
                config.Save();
                return config;
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, SerializerSettings);
            File.WriteAllText(Plugin.ConfigDirectories.ConfigPath, json);
        }
    }
}
