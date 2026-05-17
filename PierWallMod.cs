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
        registrator.RegisterData<PierWallData>();
        registrator.RegisterDataWithInterface<IResearchNodesData>();
    }

    /// <inheritdoc />
    public override void MigrateJsonConfig(VersionSlim savedVersion, Dict<string, object> savedValues)
    {
        // No JSON configuration fields are used by the mod at the moment.
    }
    #endregion
}
