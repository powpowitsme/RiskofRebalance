using BepInEx;
using BepInEx.Logging;
using R2API;
using R2API.Utils;
using RiskofRebalance.Managers;
using RoR2;

namespace RiskofRebalance
{
    [BepInPlugin("com.powpowitsme.RiskofRebalance", "Risk of Rebalance", "0.1.0")]
    [BepInDependency(RecalculateStatsAPI.PluginGUID, LanguageAPI.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class RiskofRebalance : BaseUnityPlugin
    {
        public static ItemDef emptyItemDef = null;
        public static BuffDef emptyBuffDef = null;
        public static PluginInfo pluginInfo;
        internal static ManualLogSource logger;

        public void Awake()
        {
            Log.Init(Logger);
            logger = base.Logger;
            Log.Message("Risk of Rebalance initialized!");
            /*
            Managers.ChangeManager instance = new();
            RoR2Application.onLoad += () => {
                instance.ChangeItemTokens();
            };
            */
            //Check this first since config relies on it

            pluginInfo = Info;
            ConfigFiles.Init();

            new ItemManager();
            new ChangeManager();
            //SetupAssists();
            AddHooks();
        }

        private void AddHooks()
        {
            On.RoR2.GlobalEventManager.OnHitEnemy += DamageManager.OnHitEnemy.GlobalEventManager_OnHitEnemy;
            RecalculateStatsAPI.GetStatCoefficients += ChangeManager.GetStatCoefficients.RecalculateStatsAPI_GetStatCoefficients;
            On.RoR2.CharacterBody.RecalculateStats += ChangeManager.RecalculateStats.CharacterBody_RecalculateStats;
            On.RoR2.HealthComponent.TakeDamage += HealthManager.TakeDamage.HealthComponent_TakeDamage;
            On.RoR2.GlobalEventManager.OnCharacterDeath += HealthManager.OnCharacterDeath.GlobalEventManager_OnCharacterDeath;
            On.RoR2.GlobalEventManager.OnHitAll += DamageManager.OnHitAll.GlobalEventManager_OnHitAll;
            On.RoR2.HealthComponent.UpdateLastHitTime += HealthManager.HealthComponent_UpdateLastHitTime.UpdateLastHitTime;

        }
    }
}