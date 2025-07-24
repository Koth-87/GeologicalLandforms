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
    private static void Add_Postfix()
    {
        WorldTileInfo.InvalidateCache();
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(WorldObjectsHolder.Remove))]
    private static void Remove_Postfix()
    {
        WorldTileInfo.InvalidateCache();
    }
}
