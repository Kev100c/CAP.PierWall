#region Header
// PierWallMod.cs
// Registers the data classes used by the Pier Wall mod.
// Reference note: created with help from GPT (ChatGPT / GPT-5 Thinking).
#endregion

#region Usings
using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Console;
using Mafi.Core.Mods;
using Mafi.Core.Research;
#endregion

namespace PierWallMod;

/// <summary>
/// Mod entry point for the Pier Wall content pack.
/// </summary>
public sealed class PierWallMod : DataOnlyMod
{
    #region Constructor
    /// <summary>
    /// Initializes the mod entry point.
    /// </summary>
    /// <param name="manifest">Loaded mod manifest.</param>
    public PierWallMod(ModManifest manifest)
        : base(manifest)
    {
        Log.Info("PierWallMod: constructed");
    }
    #endregion

    #region Registration
    /// <inheritdoc />
    public override void RegisterPrototypes(ProtoRegistrator registrator)
    {
        Log.Info("PierWallMod: registering prototypes");
        registrator.RegisterData(new PierWallData(JsonConfig));
        registrator.RegisterDataWithInterface<IResearchNodesData>();
    }
    #endregion

    [ConsoleCommand(
        documentation: "0 = Default. Takes effect after saving and reloading.",
        customCommandName: "CAP_PierWallMod_set_height_offset")]
    public string SetHeightOffset(int value)
    {
        if (!JsonConfig.TrySetValue("collision_height_offset", value, out string errorMessage))
        {
            return $"Failed to set height offset: {errorMessage}";
        }
        return $"CAP.PierWallMod: Set height offset to {value}.\n" +
            $"Savegame reload required for changes to take effect.";

    }

    [ConsoleCommand(
        documentation: "Returns the current height offset value.",
        customCommandName: "CAP_PierWallMod_get_height_offset")]
    public string GetHeightOffset()
    {
        int heightOffset = JsonConfig.GetInt("collision_height_offset", 0);
        return $"CAP.PierWallMod: Current height offset is {heightOffset}.";
    }

    [ConsoleCommand(
        documentation: "False = Default. Takes effect after saving and reloading.",
        customCommandName: "CAP_PierWallMod_set_category_sorting")]
    public string SetCategory(bool value)
    {
        if (!JsonConfig.TrySetValue("sort_in_Category", value, out string errorMessage))
        {
            return $"Failed to set category sorting: {errorMessage}";
        }
        return $"CAP.PierWallMod: Set category sorting to {value}.\n" +
            $"Savegame reload required for changes to take effect.";
    }
    [ConsoleCommand(
        documentation: "Returns the current category sorting value.",
        customCommandName: "CAP_PierWallMod_get_category_sorting")]
    public string GetCategory()
    {
        bool sortInCategory = JsonConfig.GetBool("sort_in_Category", true);
        return $"CAP.PierWallMod: Current category sorting is {sortInCategory}.";
    }
}


