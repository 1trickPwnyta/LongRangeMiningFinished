using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace LongRangeMiningFinished
{
    public class LongRangeMiningFinishedSettings : ModSettings
    {
        public enum CommunicationType
        {
            Message,
            Letter
        }

        public static CommunicationType ComType = CommunicationType.Message;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            if (listingStandard.ButtonTextLabeled("LongRangeMiningFinished_CommunicationType".Translate(), ComType.ToString()))
            {
                List<FloatMenuOption> list = new List<FloatMenuOption>();
                foreach (object obj in Enum.GetValues(typeof(CommunicationType)))
                {
                    CommunicationType type = (CommunicationType)obj;
                    list.Add(new FloatMenuOption(type.ToString(), delegate ()
                    {
                        ComType = type;
                    }));
                }
                Find.WindowStack.Add(new FloatMenu(list));
            }

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ComType, "LongRangeMiningFinished_ComType");
        }
    }
}
