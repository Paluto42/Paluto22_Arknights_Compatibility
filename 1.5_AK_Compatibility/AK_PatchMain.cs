using HarmonyLib;
using Verse;

namespace Paluto22.AK.Patch
{
    [StaticConstructorOnStartup]
    public class AK_PatchMain
    {
        static AK_PatchMain()
        {
            Harmony harmony = new Harmony("paluto22.ak.compatibility");
            harmony.PatchAll();
            Log.Message("[Arknights-AlienRaces Compability] Initialized");
        }
    }
    public class AKheadPatchesConfig : Mod
    {
        public AKheadPatchesConfig(ModContentPack content) : base(content)
        {
            Log.Message("[Arknights Compability] AlienRaces Initializing");
        }
    }
}
