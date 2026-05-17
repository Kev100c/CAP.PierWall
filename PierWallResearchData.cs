#region Header
// PierWallResearchData.cs
// Registers the research node that unlocks the pier wall variants.
// Reference note: created with help from GPT (ChatGPT / GPT-5 Thinking).
#endregion

#region Usings
using Mafi;
using Mafi.Base;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;
#endregion

namespace PierWallMod;

internal sealed class PierWallResearchData : IResearchNodesData
{
    #region Registration
    public void RegisterData(ProtoRegistrator registrator)
    {
        ProtosDb prototypesDb = registrator.PrototypesDb;

        ResearchNodeProto retainingWallsNode =
            prototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.RetainingWalls);

        PierWallProto shortWall = prototypesDb.GetOrThrow<PierWallProto>(PierWallIds.Straight1);
        PierWallProto longWall = prototypesDb.GetOrThrow<PierWallProto>(PierWallIds.Straight4);
        PierWallProto cornerWall = prototypesDb.GetOrThrow<PierWallProto>(PierWallIds.Corner);
        PierWallProto crossWall = prototypesDb.GetOrThrow<PierWallProto>(PierWallIds.Cross);
        PierWallProto teeWall = prototypesDb.GetOrThrow<PierWallProto>(PierWallIds.Tee);

        Vector2i pierWallsPosition = retainingWallsNode.GridPosition + new Vector2i(4, -4);

        ResearchNodeProto researchNode = registrator.ResearchNodeProtoBuilder
            .Start("Pier walls", PierWallIds.UnlockPierWalls, costMonths: 6)
            .Description("Unlocks pier wall variants for cleaner island edges. These walls can be placed on land and in the ocean.")
            .AddLayoutEntityToUnlock(PierWallIds.Straight1)
            .AddLayoutEntityToUnlock(PierWallIds.Straight4)
            .AddLayoutEntityToUnlock(PierWallIds.Corner)
            .AddLayoutEntityToUnlock(PierWallIds.Tee)
            .AddLayoutEntityToUnlock(PierWallIds.Cross)
            .AddIcon(shortWall)
            .AddIcon(longWall)
            .AddIcon(cornerWall)
            .AddIcon(crossWall)
            .AddIcon(teeWall)
            .AddParents(retainingWallsNode)
            .SetGridPosition(pierWallsPosition)
            .BuildAndAdd();

        Log.Info(
            $"PierWallMod Research: RetainingWallsPos={retainingWallsNode.GridPosition}, " +
            $"PierWallsPos={researchNode.GridPosition}, NodeId={researchNode.Id}");
    }
    #endregion
}
