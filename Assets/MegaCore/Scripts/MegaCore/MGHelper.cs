using UnityEngine;

namespace MegaCore
{
    public static class MGHelper
    {
        public static void LogInfo(string message)
        {
            Debug.LogFormat("MegaCore: I: {0}", message);
        }

        public static void LogError(string message)
        {
            Debug.LogErrorFormat("MegaCore: E: {0}", message);
        }

        public static void LogWarnong(string message)
        {
            Debug.LogWarningFormat("MegaCore: W: {0}", message);
        }
    }

}
