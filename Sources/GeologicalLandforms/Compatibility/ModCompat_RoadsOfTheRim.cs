#if RW_1_6_OR_GREATER

using System.Linq;
using HarmonyLib;
using LunarFramework.Patching;
using RimWorld.Planet;

namespace GeologicalLandforms.Compatibility;

[HarmonyPatch]
public class ModCompat_RoadsOfTheRim : ModCompat
{
    public override string TargetAssemblyName => "RoadsOfTheRim";
    public override string DisplayName => "Roads Of The Rim";

    [HarmonyPostfix]
    [HarmonyPatch("RoadsOfTheRim.DefModExtension_RotR_RoadDef", "ImpassableAllowed")]
    private static void ImpassableAllowed_Postfix(int tile, ref bool __result)
    {
        if (!__result)
        {
            var tileInfo = WorldTileInfo.Get(tile);
            if (tileInfo.HasLandforms() && tileInfo.Landforms.Any(lf => !lf.IsLayer))
            {
                __result = true;
            }
        }
    }
}

[HarmonyPatch]
public class ModCompat_RailsAndRoadsOfTheRim : ModCompat
{
    public override string TargetAssemblyName => "RailsAndRoadsOfTheRim";
    public override string DisplayName => "Rails And Roads Of The Rim";

    [HarmonyPostfix]
    [HarmonyPatch("RailsAndRoadsOfTheRim.DefModExtension_RotR_RoadDef", "ImpassableAllowed")]
    private static void ImpassableAllowed_Postfix(PlanetTile tile, ref bool __result)
    {
        if (!__result)
        {
            var tileInfo = WorldTileInfo.Get(tile.tileId);
            if (tileInfo.HasLandforms() && tileInfo.Landforms.Any(lf => !lf.IsLayer))
            {
                __result = true;
            }
        }
    }
}

#endif
