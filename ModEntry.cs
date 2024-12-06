using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace BuildingMenuAnywhere
{
    public class ModEntry : Mod
    {
        private ModConfig Config = null!;

        public override void Entry(IModHelper helper)
        {
            // Load the configuration.
            Config = helper.ReadConfig<ModConfig>();

            // Subscribe to the ButtonPressed event.
            helper.Events.Input.ButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            // Ensure the player is free to act and the configured key was pressed.
            if (Context.IsPlayerFree && e.Button.ToString() == Config.BuildingMenuKey)
            {
                try
                {
                    // Use "Robin" as the builder to match the game's expected context.
                    OpenCarpenterMenu("Robin", Game1.getLocationFromName("Farm"));
                }
                catch (Exception ex)
                {
                    // Log any errors to the SMAPI console.
                    Monitor.Log($"Failed to open Carpenter Menu: {ex.Message}\n{ex.StackTrace}", LogLevel.Error);
                }
            }
        }

        private void OpenCarpenterMenu(string builder, GameLocation targetLocation)
        {
            // Ensure the Farm location is properly loaded.
            if (targetLocation == null)
            {
                Monitor.Log("Failed to open Carpenter Menu: Target location is null.", LogLevel.Warn);
                return;
            }

            // Open the Carpenter Menu.
            Game1.activeClickableMenu = new CarpenterMenu(builder, targetLocation);
        }
    }

    public class ModConfig
    {
        public string BuildingMenuKey { get; set; } = "B"; // Default key to open the building menu.
    }
}
