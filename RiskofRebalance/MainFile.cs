using BepInEx;
using BepInEx.Logging;



namespace RiskofRebalance
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class MainFile : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + "RiskofRebalance";
        public const string PluginAuthor = "powpowitsme";
        public const string PluginName = "Risk of Rebalance";
        public const string PluginVersion = "1.0";
        internal static ManualLogSource logger;
        public void Awake()
        {
            Log.Init(Logger);
            logger = base.Logger;
            logger.LogMessage("Risk of Rebalance initialized!");
        }
    }
}