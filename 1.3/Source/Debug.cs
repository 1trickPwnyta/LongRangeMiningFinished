namespace LongRangeMiningFinished
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{LongRangeMiningFinishedMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
