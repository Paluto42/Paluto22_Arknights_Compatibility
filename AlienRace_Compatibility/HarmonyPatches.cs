using System;
using Verse;
using HarmonyLib;
using AK_DLL;
using AlienRace;
using UnityEngine;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace Paluto22.AK.Patch.AlienRace
{
    [StaticConstructorOnStartup]
    public class HarmonyPatches
    {
        private static readonly Type patchType = typeof(HarmonyPatches);
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("paluto22.alienrace.compatibility");
            /*MethodBase method1 = typeof(AlienRenderTreePatches).GetMethod("HeadGraphicForPrefix", new Type[] { typeof(PawnRenderNode_Head), typeof(Pawn), typeof(Graphic).MakeByRefType() });
            MethodBase method2 = typeof(AlienRenderTreePatches).GetMethod("BodyGraphicForPrefix", new Type[] { typeof(PawnRenderNode_Body), typeof(Pawn), typeof(Graphic).MakeByRefType() });
            if (method1 != null)
            {
                harmony.Patch(method1, new HarmonyMethod(patchType, "HeadGraphicForPrefix_Prefix"));
            }
            if (method2 != null)
            {
                harmony.Patch(method2, new HarmonyMethod(patchType, "BodyGraphicForPrefix_Prefix"));
            }
            */
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Message("[Arknights-AlienRaces Compability] Initialized");
        }
        /*public static bool HeadGraphicForPrefix_Prefix(ref bool __result, PawnRenderNode_Head __instance, Pawn pawn)
        {
            if (OperatorDef.currentlyGenerating || pawn.GetDoc() != null)
            {
                __result = true;
                return false;
            }
            __result = true;
            return true;
        }

        public static bool BodyGraphicForPrefix_Prefix(ref bool __result, PawnRenderNode_Body __instance, Pawn pawn)
        {
            if (OperatorDef.currentlyGenerating || pawn.GetDoc() != null)
            {
                __result = true;
                return false;
            }
            __result = true;
            return true;
        }*/

    }
}
