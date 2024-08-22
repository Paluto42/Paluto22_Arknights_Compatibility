using HarmonyLib;
using RimWorld;
using System;
using Verse;
using System.Reflection;
using AK_DLL;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Paluto22.AK.Patch
{
    [StaticConstructorOnStartup]
    public static class Paluto22_AKPatches
    {
        private static readonly Type patchType = typeof(Paluto22_AKPatches);
        static Paluto22_AKPatches()
        {
            Harmony harmony = new Harmony("paluto22.ak.compatibility");
            harmony.Patch(AccessTools.Method(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn), new[] { typeof(PawnGenerationRequest) }),
                 prefix: new HarmonyMethod(patchType, nameof(NewGeneratePawn_Prefix)));
            Log.Message("[Arknights-AlienRaces Compability] Initialized");

            //动态表情
            #region 
            if (ModLister.GetActiveModWithIdentifier("Nals.FacialAnimation") != null)
            {
                Assembly FacialAnimation = Type.GetType("FacialAnimation.FacialAnimationMod, FacialAnimation")?.Assembly;
                Type type = FacialAnimation?.GetTypes().FirstOrDefault(t => t.Name == "DrawFaceGraphicsComp");

                MethodBase method = type?.GetMethod("CompRenderNodes");
                if (method != null)
                {
                    harmony.Patch(method, postfix: new HarmonyMethod(patchType, nameof(NewPawnRender_Postfix)));
                    Log.Message("[Arknights-FacialAnimation Compability] Initialized");
                }
            }
            #endregion
            //时装柜
            #region
            if (ModLister.GetActiveModWithIdentifier("aedbia.fashionwardrobe") != null)
            {
                Assembly Fashion_Wardrobe = Type.GetType("Fashion_Wardrobe.FWSetting, Fashion Wardrobe")?.Assembly;
                Type type = Fashion_Wardrobe?.GetTypes().FirstOrDefault(t => t.FullName == "Fashion_Wardrobe.FW_Windows+SelApparelWindow");
                MethodBase DoWindowContents = type.GetMethod("DoWindowContents");
                MethodBase Close = type.GetMethod("Close");

                if (DoWindowContents != null && Close != null)
                {
                    harmony.Patch(DoWindowContents, prefix: new HarmonyMethod(patchType, nameof(FashionWardrobe_Prefix)));
                    harmony.Patch(Close, prefix: new HarmonyMethod(patchType, nameof(FashionWardrobe_Postfix)));
                    Log.Message("[Arknights-FashionWardrobe Compability] Initialized");
                }
            }
            #endregion
        }
        //方法
        public static void NewGeneratePawn_Prefix(ref PawnGenerationRequest request)
        {
            if (OperatorDef.currentlyGenerating == false && !ModsConfig.IsActive("erdelf.HumanoidAlienRaces"))
            {
                return;
            }
            if (Current.ProgramState == ProgramState.Playing && OperatorDef.currentlyGenerating == true)
            {
                request.KindDef = PawnKindDefOf.Colonist;
            }
        }
        public static void NewPawnRender_Postfix(ref List<PawnRenderNode> __result, Pawn ___pawn)
        {
            if (___pawn.GetDoc() != null )
            {
                if (!___pawn.GetDoc().operatorDef.defName.StartsWith("AK"))
                {
                    return;
                }
                __result = null;
            }
        }
        public static void FashionWardrobe_Prefix(Rect inRect)
        {
            if (!OperatorDef.currentlyGenerating)
            {
                OperatorDef.currentlyGenerating = true;
            }
        }
        public static void FashionWardrobe_Postfix(bool doCloseSound = true)
        {
            if (OperatorDef.currentlyGenerating) 
            {
                OperatorDef.currentlyGenerating = false;
            }
        }
    }
}
