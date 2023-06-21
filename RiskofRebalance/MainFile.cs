using BepInEx;
using BepInEx.Logging;
using RoR2;



namespace RiskofRebalance
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class MainFile : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + "RiskofRebalance";
        public const string PluginAuthor = "powpowitsme";
        public const string PluginName = "Risk of Rebalance";
        public const string PluginVersion = "0.1.0";
        internal static ManualLogSource logger;
        public void Awake()
        {
            Log.Init(Logger);
            logger = base.Logger;
            Log.Message("Risk of Rebalance initialized!");
            ChangeManager instance = new();
            RoR2Application.onLoad += () => {
                instance.ChangeItemTokens();
            };
        }
    }
}