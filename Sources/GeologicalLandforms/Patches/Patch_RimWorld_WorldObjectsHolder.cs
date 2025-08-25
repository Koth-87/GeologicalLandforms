using HarmonyLib;
using LunarFramework.Patching;
using RimWorld.Planet;

namespace GeologicalLandforms.Patches;

[PatchGroup("Main")]
[HarmonyPatch(typeof(WorldObjectsHolder))]
internal static class Patch_RimWorld_WorldObjectsHolder
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(WorldObjectsHolder.Add))]
    private static void Add_Postfix(WorldObject o)
    {
        #if RW_1_6_OR_GREATER
        if (!o.Tile.Layer.IsRootSurface) return;
        #endif

        WorldTileInfo.InvalidateCache(o.Tile.tileId);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(WorldObjectsHolder.Remove))]
    private static void Remove_Postfix(WorldObject o)
    {
        #if RW_1_6_OR_GREATER
        if (!o.Tile.Layer.IsRootSurface) return;
        #endif

        WorldTileInfo.InvalidateCache(o.Tile.tileId);
    }
}
