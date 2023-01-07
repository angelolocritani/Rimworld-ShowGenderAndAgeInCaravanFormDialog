using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using HarmonyLib;
using System;

namespace ShowGenderAndAgeInCaravanFormDialog
{


    [StaticConstructorOnStartup]
    public static class ShowGenderAgeCaravanFormDialogMod
    {
        public static Harmony harmonyInstance;

        static ShowGenderAgeCaravanFormDialogMod()
        {
            harmonyInstance = new Harmony("arl85.ShowGenderAndAgeInCaravanFormDialog");
            harmonyInstance.PatchAll();
        }


        [HarmonyPatch(typeof(TransferableOneWay), "Label", MethodType.Getter)]
        public static class TransferableOneWay_Label
        {
            static void Postfix(ref string __result, TransferableOneWay __instance)
            {

                if (__instance.AnyThing is Pawn pawn)
                {
                    if (pawn.Name != null && !pawn.Name.Numerical && !pawn.RaceProps.Humanlike)
                    {
                        __result += ", " + pawn.def.label;
                    }
                 
                    __result += " (" + pawn.GetGenderLabel() + ", " + Mathf.FloorToInt(pawn.ageTracker.AgeBiologicalYearsFloat) + ")";

                }
            }


        }

    }


}

