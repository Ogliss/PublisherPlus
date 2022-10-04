using HarmonyLib;
using PublisherPlus.Interface;
using RimWorld;
using System;
using System.Linq;
using Verse;
using Verse.Sound;

namespace PublisherPlus.Patch
{
    /*
    [HarmonyPatch(typeof(Dialog_MessageBox), "CreateConfirmation")]
    internal static class Verse_Dialog_MessageBox_CreateConfirmation
    {
        private static bool Prefix(Dialog_MessageBox __instance, ref Window __result, TaggedString text)
        {
            Log.Message(text);
            if (!(__instance is Dialog_ConfirmModUpload)) { return true; }

            var selectedMod = Access.GetSelectedMod();
            if (selectedMod == null) { return false; }
            __result = new Dialog_Publish(selectedMod.GetWorkshopItemHook());

            return false;
        }
    }
    */ // TaggedString text, Action confirmedAct, bool destructive = false, string title = null
    [HarmonyPatch(typeof(Dialog_ConfirmModUpload), MethodType.Constructor)]
    [HarmonyPatch(new Type[] { typeof(ModMetaData), typeof(Action) })]
    internal static class Verse_Dialog_MessageBox_Dialog_ConfirmModUpload
    {
        private static void Prefix(Dialog_ConfirmModUpload __instance, ref Action acceptAction)
        {
            var selectedMod = Access.GetSelectedMod();
            if (selectedMod == null) return;
            acceptAction = delegate()
            {
                SoundDefOf.Tick_High.PlayOneShotOnCamera(null);
                Find.WindowStack.Add(new Dialog_Publish(selectedMod.GetWorkshopItemHook()));
            };

        }
    }
}
