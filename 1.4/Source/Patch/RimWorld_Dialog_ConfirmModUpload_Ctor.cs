﻿using System;
using HarmonyLib;
using PublisherPlus.Interface;
using RimWorld;
using Verse;

namespace PublisherPlus.Patch
{
    [HarmonyPatch(typeof(Dialog_ConfirmModUpload), MethodType.Constructor, typeof(ModMetaData), typeof(Action))]
    internal static class RimWorld_Dialog_ConfirmModUpload_Ctor
    {
        private static void Postfix(ref Dialog_ConfirmModUpload __instance, ModMetaData ___mod)
        {
            void Action() => Find.WindowStack.Add(new Dialog_Publish(___mod.GetWorkshopItemHook()));

            __instance.buttonAAction = Action;
            __instance.acceptAction = Action;
        }
    }
}
