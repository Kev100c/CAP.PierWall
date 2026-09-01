#region Header
// PierWallIds.cs
// Stores the prototype and research IDs used by the mod.
// Reference note: created with help from GPT (ChatGPT / GPT-5 Thinking).
#endregion

#region Usings
using Mafi.Base;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using ResearchNodeId = Mafi.Core.Research.ResearchNodeProto.ID;
#endregion

namespace PierWallMod;

internal static class PierWallIds
{
    #region Category IDs
    public static readonly ToolbarCategoryProto.ID PierWallCategory = new("PierWall");
    #endregion
    #region Building IDs
    public static readonly StaticEntityProto.ID Straight1 = new("PierWallStraight1");
    public static readonly StaticEntityProto.ID Straight4 = new("PierWallStraight4");
    public static readonly StaticEntityProto.ID Corner = new("PierWallCorner");
    public static readonly StaticEntityProto.ID Cross = new("PierWallCross");
    public static readonly StaticEntityProto.ID Tee = new("PierWallTee");
    #endregion

    #region Research IDs
    public static readonly ResearchNodeId UnlockPierWalls = Ids.Research.CreateId("UnlockPierWalls");
    #endregion
}
