#region Header
// PierWallProto.cs
// Defines the custom wall prototype used by the mod.
// The type inherits from RetainingWallProto so the game continues to use
// retaining-wall-specific placement and entity behavior.
// Reference note: created with help from GPT (ChatGPT / GPT-5 Thinking).
#endregion

#region Usings
using System;
using System.Reflection;
using Mafi.Base.Prototypes.Buildings;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Prototypes;
#endregion

namespace PierWallMod;

/// <summary>
/// Retaining wall prototype used for the pier wall variants.
/// </summary>
/// <remarks>
/// The base game retaining wall prototype hard-codes flood destruction to <c>false</c>.
/// This implementation keeps the retaining wall type and updates the inherited field after
/// construction so the same placement path is preserved while flood resistance is enabled.
/// </remarks>
public sealed class PierWallProto : RetainingWallProto
{
    #region Constructor
    /// <summary>
    /// Initializes a pier wall prototype instance.
    /// </summary>
    /// <param name="id">Prototype identifier.</param>
    /// <param name="strings">Localized strings.</param>
    /// <param name="layout">Entity layout definition.</param>
    /// <param name="costs">Construction costs.</param>
    /// <param name="graphics">Graphics definition.</param>
    public PierWallProto(
        ID id,
        Str strings,
        EntityLayout layout,
        EntityCosts costs,
        Gfx graphics)
        : base(id, strings, layout, costs, graphics)
    {
        FieldInfo floodResistanceField = typeof(StaticEntityProto).GetField(
            nameof(StaticEntityProto.CannotBeDestroyedByFlood),
            BindingFlags.Instance | BindingFlags.Public)
            ?? throw new InvalidOperationException(
                "The field 'CannotBeDestroyedByFlood' was not found on StaticEntityProto.");

        floodResistanceField.SetValue(this, true);
    }
    #endregion
}
