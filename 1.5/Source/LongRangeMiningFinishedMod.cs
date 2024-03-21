using Verse;
using HarmonyLib;
using UnityEngine;

namespace LongRangeMiningFinished
{
    public class LongRangeMiningFinishedMod : Mod
    {
        public const string PACKAGE_ID = "longrangeminingfinished.1trickPwnyta";
        public const string PACKAGE_NAME = "Long-Range Mining Finished";

        public static LongRangeMiningFinishedSettings Settings;

        public LongRangeMiningFinishedMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<LongRangeMiningFinishedSettings>();

            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            LongRangeMiningFinishedSettings.DoSettingsWindowContents(inRect);
        }
    }
}
