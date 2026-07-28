#region Header
// PierWallData.cs
// Registers the pier wall building variants and their layouts.
// Reference note: created with help from GPT (ChatGPT / GPT-5 Thinking).
#endregion

#region Usings
using Mafi;
using Mafi.Base;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Localization;
#endregion

namespace PierWallMod;

internal sealed class PierWallData : IModData
{
    #region Constants
    private const int RetainedHeightTiles = 5;

    private const string IconRoot = "Assets/PierWallMod/Icons";
    private const string IconShort = IconRoot + "/PierWallStraight1.png";
    private const string IconLong = IconRoot + "/PierWallStraight4.png";
    private const string IconCorner = IconRoot + "/PierWallCorner.png";
    private const string IconCross = IconRoot + "/PierWallCross.png";
    private const string IconTee = IconRoot + "/PierWallTee.png";
    #endregion

    #region Registration
    public void RegisterData(ProtoRegistrator registrator) 
    {
        ProtosDb prototypesDb = registrator.PrototypesDb;
        ImmutableArray<ToolbarEntryData> terraformCategories =
            registrator.GetCategoriesProtos(IdsCore.ToolbarCategories.Terraforming);

        PierWallProto shortWall = prototypesDb.Add(
            new PierWallProto(
                PierWallIds.Straight1,
                CreateStrings(PierWallIds.Straight1, "Pier wall (short)"),
                CreateLayout(registrator, 1, 1, "..", "##", ".."),
                Costs.Buildings.RetainingWall1.MapToEntityCosts(registrator),
                CreateGraphics(terraformCategories, "Assets/Base/Buildings/RetainingWalls/RetainingWall2m.prefab", IconShort)));

        PierWallProto longWall = prototypesDb.Add(
            new PierWallProto(
                PierWallIds.Straight4,
                CreateStrings(PierWallIds.Straight4, "Pier wall (long)"),
                CreateLayout(registrator, 4, 3, ".....", "#####", "....."),
                Costs.Buildings.RetainingWall4.MapToEntityCosts(registrator),
                CreateGraphics(terraformCategories, "Assets/Base/Buildings/RetainingWalls/RetainingWall8m.prefab", IconLong)));

        PierWallProto cornerWall = prototypesDb.Add(
            new PierWallProto(
                PierWallIds.Corner,
                CreateStrings(PierWallIds.Corner, "Pier wall (corner)"),
                CreateLayout(registrator, 2, 0, ".#.", ".##", "..."),
                Costs.Buildings.RetainingWall2.MapToEntityCosts(registrator),
                CreateGraphics(terraformCategories, "Assets/Base/Buildings/RetainingWalls/RetainingWallCorner.prefab", IconCorner)));

        PierWallProto crossWall = prototypesDb.Add(
            new PierWallProto(
                PierWallIds.Cross,
                CreateStrings(PierWallIds.Cross, "Pier wall (cross)"),
                CreateLayout(registrator, 2, 0, ".#.", "###", ".#."),
                Costs.Buildings.RetainingWall2.MapToEntityCosts(registrator),
                CreateGraphics(terraformCategories, "Assets/Base/Buildings/RetainingWalls/RetainingWallXing.prefab", IconCross)));

        PierWallProto teeWall = prototypesDb.Add(
            new PierWallProto(
                PierWallIds.Tee,
                CreateStrings(PierWallIds.Tee, "Pier wall (tee)"),
                CreateLayout(registrator, 2, 0, ".#.", "###", "..."),
                Costs.Buildings.RetainingWall2.MapToEntityCosts(registrator),
                CreateGraphics(terraformCategories, "Assets/Base/Buildings/RetainingWalls/RetainingWallTee.prefab", IconTee)));

        shortWall.SetNextTierIndirect(longWall);
        longWall.SetNextTierIndirect(cornerWall);
        cornerWall.SetNextTierIndirect(crossWall);
        crossWall.SetNextTierIndirect(teeWall);
    }
    #endregion

    #region Helpers
    private static Proto.Str CreateStrings(StaticEntityProto.ID id, string title)
    {
        LocStr description = LocalizationManager.CreateAlreadyLocalizedStr(
            $"{id.Value}__desc",
            $"Can be placed on land and in the ocean. Useful for cleaner island edges. Holds up to {RetainedHeightTiles} units of retained height.");

        return Proto.CreateStr(id, title, description);
    }

    private static LayoutEntityProto.Gfx CreateGraphics(
        ImmutableArray<ToolbarEntryData> categories,
        string prefabPath,
        string iconPath)
    {
        return new LayoutEntityProto.Gfx(
            prefabPath: prefabPath,
            customIconPath: iconPath,
            categories: categories,
            useInstancedRendering: true);
    }

    private static EntityLayout CreateLayout(
        ProtoRegistrator registrator,
        int wallLengthTiles,
        int collapseThreshold,
        params string[] retainingVerticesLayout)
    {
        string tokenLine = "(W)".RepeatString(wallLengthTiles);

        return registrator.LayoutParser.ParseLayoutOrThrow(
            new EntityLayoutParams(
                customTokens:
                [
                    new CustomLayoutToken(
                        "(W)",
                        (_, _) => new LayoutTokenSpec(
                            heightFrom: -6,
                            heightToExcl: 1,
                            constraint: LayoutTileConstraint.Ground
                                      | LayoutTileConstraint.Ocean
                                      | LayoutTileConstraint.NoRubbleAfterCollapse,
                            terrainSurfaceHeight: null,
                            minTerrainHeight: -5,
                            maxTerrainHeight: 0))
                ],
                customVertexDataLayout: retainingVerticesLayout,
                customCollapseVerticesThreshold: collapseThreshold,
                customVertexTransformFn: (vertex, token) => token == '#'
                    ? vertex.WithExtraConstraint(LayoutTileConstraint.DisableTerrainPhysics)
                    : vertex),
            tokenLine,
            tokenLine);
    }
    #endregion
}
