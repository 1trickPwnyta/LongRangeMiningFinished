using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace LongRangeMiningFinished
{
    [HarmonyPatch(typeof(JobDriver_Mine))]
    [HarmonyPatch("MakeNewToils")]
    public static class Patch_JobDriver_Mine
    {
        public static void Postfix(JobDriver_Mine __instance, ref IEnumerable<Toil> __result)
        {
            Toil mineToil = __result.Last();
            Action tickAction = mineToil.tickAction;
            mineToil.tickAction = delegate ()
            {
                tickAction();

                if (__instance.job == null)
                {
                    Pawn actor = mineToil.actor;
                    if (!actor.Map.IsPlayerHome && actor.Map.AllCells.Where(cell => actor.Map.designationManager.DesignationAt(cell, DesignationDefOf.Mine) != null || actor.Map.designationManager.DesignationAt(cell, DesignationDefOf.MineVein) != null).Count() == 0)
                    {
                        Debug.Log("com type = " + LongRangeMiningFinishedSettings.ComType);
                        switch (LongRangeMiningFinishedSettings.ComType)
                        {
                            case LongRangeMiningFinishedSettings.CommunicationType.Message:
                                Messages.Message(
                                    "LongRangeMiningFinished_Communication".Translate(actor.Name.ToStringShort),
                                    actor,
                                    MessageTypeDefOf.NeutralEvent);
                                break;
                            case LongRangeMiningFinishedSettings.CommunicationType.Letter:
                                Find.LetterStack.ReceiveLetter(
                                    "LongRangeMiningFinished_LetterLabel".Translate(),
                                    "LongRangeMiningFinished_Communication".Translate(actor.Name.ToStringShort),
                                    LetterDefOf.NeutralEvent,
                                    actor);
                                break;
                        }
                    }
                }
            };

            List<Toil> toils = __result.ToList();
            toils.RemoveLast();
            toils.Add(mineToil);
            __result = toils;
        }
    }
}
