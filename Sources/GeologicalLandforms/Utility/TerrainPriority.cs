#if RW_1_6_OR_GREATER

using Verse;

namespace GeologicalLandforms;

public static class TerrainPriority
{
    public static readonly PriorityOptions DefaultOptions = new();

    public static TerrainDef Apply(TerrainDef a, TerrainDef b, PriorityOptions options)
    {
        if (a == null) return b;
        if (b == null) return a;

        var aWater = a.IsWater;
        var bWater = b.IsWater;

        if (aWater && bWater)
        {
            var aDepth = WaterDepthFor(a);
            var bDepth = WaterDepthFor(b);

            if (aDepth > bDepth) return a;
            if (bDepth > aDepth) return b;

            var aMarshy = a.HasTag("WaterMarshy");
            var bMarshy = b.HasTag("WaterMarshy");

            if (aMarshy != bMarshy)
                return aMarshy ? b : a;

            var aRiver = a.IsRiver;
            var bRiver = b.IsRiver;

            if (aRiver != bRiver)
                return aRiver ^ options.PrioritizeMovingWater ? b : a;

            if (a.waterBodyType != b.waterBodyType)
                return a.waterBodyType > b.waterBodyType ? a : b;
        }
        else if (options.PrioritizeWater)
        {
            if (aWater) return a;
            if (bWater) return b;
        }

        if (options.PrioritizeIce)
        {
            var aIce = a.IsIce;
            var bIce = b.IsIce;

            if (aIce != bIce)
                return aIce ? a : b;
        }

        if (options.PrioritizeFertility && a.fertility != b.fertility)
        {
            return (a.fertility > b.fertility) ^ options.InvertFertility ? a : b;
        }

        return b;
    }

    public static WaterDepth WaterDepthFor(TerrainDef terrain)
    {
        if (terrain.defName.Contains("ChestDeep")) return WaterDepth.ChestDeep;
        if (terrain.defName.Contains("Deep")) return WaterDepth.Deep;
        return WaterDepth.Shallow;
    }

    public struct PriorityOptions
    {
        public bool PrioritizeWater = true;
        public bool PrioritizeMovingWater = false;
        public bool PrioritizeIce = false;
        public bool PrioritizeFertility = false;
        public bool InvertFertility = false;

        public PriorityOptions() {}
    }

    public enum WaterDepth
    {
        Shallow, ChestDeep, Deep
    }
}

#endif
