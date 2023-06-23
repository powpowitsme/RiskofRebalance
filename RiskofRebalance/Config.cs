using BepInEx.Configuration;
using RiskofRebalance.Items.Common;
using RiskofRebalance.Items.Void;
using RiskofRebalance.Managers;
///using RiskOfOptions;

namespace RiskofRebalance
{
    public static class ConfigFiles
    {
        public static ConfigFile ItemCfg;
        public static ConfigFile SurvivorCfg;
        public static ConfigFile MonsterCfg;
        public static ConfigFile SpawnpoolCfg;
        public static ConfigFile GeneralCfg;

        //public static ConfigFile SurvivorCrocoCfg;

        public static string ConfigFolderPath { get => System.IO.Path.Combine(BepInEx.Paths.ConfigPath, RiskofRebalance.pluginInfo.Metadata.GUID); }

        private const string coreModuleString = "00. Core Modules";
        /*
        private const string gameMechString = "01. Game Mechanics";
        private const string scalingString = "02. Run Scaling";
        private const string interactString = "03. Interactables";
        private const string allyString = "04. Allies";
        private const string artifactString = "05. Artifacts";
        private const string voidFieldsString = "06. Void Fields";
        private const string moonString = "07. Moon";
        private const string voidLocusString = "08. Void Locus";
        private const string miscString = "99. Misc Tweaks";
        */

        private const string commonString = "Items - Common";
        private const string voidString = "Items - Void";
        private const string itemConfigDescString = "Enable changes to this item.";

        /*
        private const string fireSelectString = "Firemode Selection (Client-Side)";
        private const string commandoString = "Survivors: Commando";
        private const string huntressString = "Survivors: Huntress";
        private const string toolbotString = "Survivors: MUL-T";
        private const string engiString = "Survivors: Engineer";
        private const string treebotString = "Survivors: REX";
        private const string crocoString = "Survivors: Acrid";
        private const string banditString = "Survivors: Bandit";
        private const string captainString = "Survivors: Captain";
        private const string mageString = "Survivors: Artificer";
        private const string loaderString = "Survivors: Loader";
        private const string mercString = "Survivors: Mercenary";
        private const string railgunnerString = "Survivors: Railgunner";
        private const string voidFiendString = "Survivors: Void Fiend";

        private const string monsterString = "Monsters";
        private const string monsterGeneralString = "General";
        private const string monsterMithrixString = "Mithrix";
        private const string monsterVoidlingString = "Voidling";
        */

        public static void Init()
        {
            ConfigGeneral();
            ConfigItems();
            ConfigSurvivors();

            ///if (SoftDependencies.RiskOfOptionsLoaded) RiskOfOptionsCompat();
        }
        /*
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void RiskOfOptionsCompat()
        {
            ModSettingsManager.SetModIcon(Content.Assets.MiscSprites.ModIcon);
        }
        */

        private static void ConfigGeneral()
        {
            GeneralCfg = new ConfigFile(System.IO.Path.Combine(ConfigFolderPath, $"RiskofRebalance_General.cfg"), true);

            //Core Modules
            ItemManager.enabled = GeneralCfg.Bind(coreModuleString, "Item Changes", true, "Enable item changes.").Value;

            //Game Mechanics

            //Run Scaling

            //Allies

            //Interactables

            //Artifacts

            //Void Fields

            //Moon

            //Void Locus

            //Misc

        }

        private static void ConfigItems()
        {
            ItemCfg = new ConfigFile(System.IO.Path.Combine(ConfigFolderPath, $"RiskofRebalance_Items.cfg"), true);
            ConfigCommonItems();
            ConfigUncommonItems();
            ConfigLegendaryItems();
            ConfigBossItems();
            ConfigLunars();
            ConfigEquipment();
            ConfigVoidItems();
        }

        private static void ConfigCommonItems()
        {
            BisonSteak.enabled = ItemCfg.Bind(commonString, "Bison Steak", true, itemConfigDescString).Value;
        }

        private static void ConfigUncommonItems()
        {

        }

        private static void ConfigLegendaryItems()
        {
        }

        private static void ConfigVoidItems()
        {
            ///PlasmaShrimp.enabled = ItemCfg.Bind(voidString, "Plasma Shrimp", true, itemConfigDescString).Value;
            Polylute.enabled = ItemCfg.Bind(voidString, "Polylute", true, itemConfigDescString).Value;
        }

        private static void ConfigBossItems()
        {
        }

        private static void ConfigLunars()
        {
            /*Currently split into a separate mod. Will likely remain that way
            Gesture.enabled = ItemCfg.Bind(lunarString, "Gesture of the Drowned", true, itemConfigDescString).Value;
            Gesture.enabled = false;

            BrittleCrown.enabled = ItemCfg.Bind(lunarString, "Brittle Crown", true, itemConfigDescString).Value;
            Meteorite.enabled = ItemCfg.Bind(lunarString, "Glowing Meteorite", true, itemConfigDescString).Value;
            ShapedGlass.enabled = ItemCfg.Bind(lunarString, "Shaped Glass", true, itemConfigDescString).Value;
            Transcendence.enabled = ItemCfg.Bind(lunarString, "Transcendence", true, itemConfigDescString).Value;
            Transcendence.alwaysShieldGate = ItemCfg.Bind(lunarString, "Transcendence - Always Shield Gate", true, "Enables shieldgating on this item even when shield gating is disabled.").Value;
            Visions.enabled = ItemCfg.Bind(lunarString, "Visions of Heresy", true, itemConfigDescString).Value;
            */
        }

        private static void ConfigEquipment()
        {
        }

        private static void ConfigFireSelect()
        {
        }

        private static void ConfigSurvivors()
        {
            SurvivorCfg = new ConfigFile(System.IO.Path.Combine(ConfigFolderPath, $"RiskofRebalance_Survivors.cfg"), true);
            /*
            CommandoCore.enabled = SurvivorCfg.Bind(commandoString, "Enable Changes", true, "Enable changes to this survivor.").Value;
            CommandoCore.fixPrimaryFireRate = SurvivorCfg.Bind(commandoString, "Double Tap - Fix Scaling", true, "Fixes Double Tap having a low attack speed cap.").Value;
            CommandoCore.phaseRoundChanges = SurvivorCfg.Bind(commandoString, "Phase Round Changes", true, "Enable changes to this skill.").Value;
            CommandoCore.rollChanges = SurvivorCfg.Bind(commandoString, "Tactical Dive Changes", true, "Enable changes to this skill.").Value;
            CommandoCore.suppressiveChanges = SurvivorCfg.Bind(commandoString, "Suppressive Fire Changes", true, "Enable changes to this skill.").Value;
            CommandoCore.grenadeChanges = SurvivorCfg.Bind(commandoString, "Frag Grenade Changes", true, "Enable changes to this skill.").Value;

            CrocoCore.Cfg.enabled = SurvivorCfg.Bind(crocoString, "0. Use Separate Config", false, "Generate a separate config file for more in-depth tuning. Overwrites ALL settings for this survivor.").Value;
            CrocoCore.enabled = SurvivorCfg.Bind(crocoString, "Enable Changes", true, "Enable changes to this survivor. Skill options unavailable due to all the changes being too interlinked.").Value;
            CrocoCore.gameplayRework = SurvivorCfg.Bind(crocoString, "Gameplay Rework", true, "A full rework of Acrid's skills.").Value;
            BiggerMeleeHitbox.enabled = SurvivorCfg.Bind(crocoString, "Extend Melee Hitbox", true, "Extends Acrid's melee hitbox so he can hit Vagrants while standing on top of them.").Value;
            BlightStack.enabled = SurvivorCfg.Bind(crocoString, "Blight Duration Reset", true, "Blight stacks like Bleed.").Value;
            RemovePoisonDamageCap.enabled = SurvivorCfg.Bind(crocoString, "Remove Poison Damage Cap", true, "Poison no longer has a hidden damage cap.").Value;
            BiggerLeapHitbox.enabled = SurvivorCfg.Bind(crocoString, "Extend Leap Collision Box", true, "Acrid's Shift skills have a larger collision hitbox. Damage radius remains the same.").Value;
            ShiftAirControl.enabled = SurvivorCfg.Bind(crocoString, "Leap Air Control", true, "Acrid's Shift skills gain increased air control at high move speeds.").Value;

            if (CrocoCore.Cfg.enabled)
            {
                GenerateCrocoConfig();
            }

            CaptainCore.enabled = SurvivorCfg.Bind(captainString, "Enable Changes", true, "Enable changes to this survivor.").Value;
            CaptainOrbitalHiddenRealms.enabled = SurvivorCfg.Bind(captainString, "Hidden Realm Orbital Skills", true, "Allow Orbital skills in Hiden Realms.").Value;
            Microbots.deletionRestrictions = SurvivorCfg.Bind(captainString, "Defensive Microbots Nerf", true, "Defensive Microbots no longer deletes stationary projectiles like gas clouds and Void Reaver mortars.").Value;
            Microbots.droneScaling = SurvivorCfg.Bind(captainString, "Defensive Microbots Drone Scaling", true, "Defensive Microbots scale with drone count instead of attack speed.").Value;
            CaptainCore.enablePrimarySkillChanges = SurvivorCfg.Bind(captainString, "Enable Primary Skill Changes", true, "Enable primary skill changes for this survivor.").Value;
            CaptainCore.modifyTaser = SurvivorCfg.Bind(captainString, "Power Taser Changes", true, "Enable changes to this skill.").Value;
            CaptainCore.nukeAmmopackNerf = SurvivorCfg.Bind(captainString, "Diablo Strike Ammopack Nerf", true, "Ammopacks only restore half of Diablo Strike's charge. Intended for use with Beacon: Resupply changes.").Value;
            CaptainCore.nukeProc = SurvivorCfg.Bind(captainString, "Diablo Strike Proc Coeficient", true, "Increases Diablo Strike's proc coefficient.").Value;

            BeaconRework.healCooldown = SurvivorCfg.Bind(captainString, "Beacon: Healing - Enable Cooldown", true, "Allow this beacon to be re-used on a cooldown.").Value;

            BeaconRework.hackCooldown = SurvivorCfg.Bind(captainString, "Beacon: Hack - Enable Cooldown", true, "Allow this beacon to be re-used on a cooldown.").Value;
            BeaconRework.hackChanges = SurvivorCfg.Bind(captainString, "Beacon: Hack - Enable Changes", true, "Enable changes to the effect of this beacon.").Value;
            BeaconRework.hackDisable = SurvivorCfg.Bind(captainString, "Beacon: Hack - Disable", false, "Removes this Beacon from the game.").Value;

            BeaconRework.shockCooldown = SurvivorCfg.Bind(captainString, "Beacon: Shocking - Enable Cooldown", true, "Allow this beacon to be re-used on a cooldown.").Value;
            BeaconRework.shockChanges = SurvivorCfg.Bind(captainString, "Beacon: Shocking - Enable Changes", true, "Enable changes to the effect of this beacon.").Value;

            BeaconRework.resupplyCooldown = SurvivorCfg.Bind(captainString, "Beacon: Resupply - Enable Cooldown", true, "Allow this beacon to be re-used on a cooldown.").Value;
            BeaconRework.resupplyChanges = SurvivorCfg.Bind(captainString, "Beacon: Resupply - Enable Changes", true, "Enable changes to the effect of this beacon.").Value;

            CaptainCore.beaconRework = SurvivorCfg.Bind(captainString, "Beacon Changes", true, "Beacons can be replaced on a cooldown and reworks Supply and Hack beacons. Disabling this disables all beacon-related changes.").Value;
            CaptainCore.beaconRework = CaptainCore.beaconRework
                && (BeaconRework.healCooldown
                || BeaconRework.hackCooldown || BeaconRework.hackChanges
                || BeaconRework.shockCooldown || BeaconRework.shockChanges
                || BeaconRework.resupplyChanges || BeaconRework.resupplyCooldown);
            BeaconRework.CaptainDeployableManager.allowLysateStack = SurvivorCfg.Bind(captainString, "Beacon Changes - Infinite Lysate Cell Stacking", false, "If Beacon Changes are enabled, allow stocks to be infinitely increased with Lysate Cells.").Value;

            Bandit2Core.enabled = SurvivorCfg.Bind(banditString, "Enable Changes", true, "Enable changes to this survivor.").Value;
            Bandit2Core.modifyStats = SurvivorCfg.Bind(banditString, "Modify Base Stats", true, "Enable base stat changes for this survivor.").Value;
            BanditSpecialGracePeriod.enabled = SurvivorCfg.Bind(banditString, "Special Grace Period", true, "Special On-kill effects can trigger if an enemy dies shortly after being hit.").Value;
            BanditSpecialGracePeriod.durationLocalUser = SurvivorCfg.Bind(banditString, "Special Grace Period Duration (Host)", 0.5f, "Length in seconds of Special Grace Period.").Value;
            BanditSpecialGracePeriod.durationOnline = SurvivorCfg.Bind(banditString, "Special Grace Period Duration (Client)", 1f, "Length in seconds of Special Grace Period for Online Clients.").Value;

            //Not sure if the Special even works if you disable this.
            if (!BanditSpecialGracePeriod.enabled)
            {
                BanditSpecialGracePeriod.enabled = true;
                BanditSpecialGracePeriod.durationOnline = 0f;
                BanditSpecialGracePeriod.durationLocalUser = 0f;
            }

            DesperadoRework.enabled = SurvivorCfg.Bind(banditString, "Desperado Persist", false, "Desperado stacks are weaker but last between stages.").Value;
            DesperadoRework.damagePerBuff = SurvivorCfg.Bind(banditString, "Desperado Persist - Damage Multiplier", 0.01f, "Revolver damage multiplier per Desperado stack if Desperado Persist is enabled.").Value;

            BackstabRework.enabled = SurvivorCfg.Bind(banditString, "Backstab Changes", true, "Backstabs minicrit for 50% bonus damage and crit chance is converted to crit damage.").Value;
            BuffHemorrhage.ignoreArmor = SurvivorCfg.Bind(banditString, "Hemmorrhage - Ignore Armor", true, "Hemmorrhage damage ignores positive armor.").Value;
            BuffHemorrhage.enableProcs = SurvivorCfg.Bind(banditString, "Hemmorrhage - Enable Procs", true, "Hemmorrhage damage has a low proc coefficient.").Value;

            //BuffHemorrhage.enableCrit = SurvivorCfg.Bind(banditString, "Hemmorrhage - Count as Crit", true, "Hemmorrhagedamage counts as crits.").Value;
            BuffHemorrhage.enableCrit = false;  //hitsound is obnoxious

            Bandit2Core.burstChanges = SurvivorCfg.Bind(banditString, "Burst Changes", true, "Enable changes to this skill.").Value;
            Bandit2Core.blastChanges = SurvivorCfg.Bind(banditString, "Blast Changes", true, "Enable changes to this skill.").Value;
            Bandit2Core.noKnifeCancel = SurvivorCfg.Bind(banditString, "Knife While Reloading", true, "Knife skills can be used without interrupting your reload.").Value;
            Bandit2Core.knifeChanges = SurvivorCfg.Bind(banditString, "Serrated Dagger Changes", true, "Enable changes to this skill.").Value;
            Bandit2Core.knifeThrowChanges = SurvivorCfg.Bind(banditString, "Serrated Shiv Changes", true, "Enable changes to this skill.").Value;
            Bandit2Core.utilityFix = SurvivorCfg.Bind(banditString, "Smokebomb Fix", true, "Fixes various bugs with Smokebomb.").Value;
            Bandit2Core.specialRework = SurvivorCfg.Bind(banditString, "Special Rework", true, "Makes Resets/Desperado a selectable passive and adds a new Special skill.").Value;

            RailgunnerCore.enabled = SurvivorCfg.Bind(railgunnerString, "Enable Changes", true, "Enable changes to this survivor.").Value;
            Survivors.DLC1.Railgunner.FixBungus.enabled = SurvivorCfg.Bind(railgunnerString, "Fix Bungus", true, "Removes self knockback force when on the ground.").Value;
            RailgunnerCore.slowFieldChanges = SurvivorCfg.Bind(railgunnerString, "Polar Field Device Changes", true, "Enable changes to this skill.").Value;
            */

            ConfigFireSelect();
        }

        /*
        private static void GenerateCrocoConfig()
        {
            SurvivorCrocoCfg = new ConfigFile(System.IO.Path.Combine(ConfigFolderPath, $"RiskyMod_Survivors_Acrid.cfg"), true);
            
            CrocoCore.enabled = SurvivorCrocoCfg.Bind("00. General", "Enable Changes", true, "Enable changes to this survivor.").Value;
            CrocoCore.gameplayRework = SurvivorCrocoCfg.Bind("00. General", "Gameplay Rework", true, "A full rework of Acrid's skills. Every option outside of General/Stats requires this to be enabled.").Value;
            BiggerMeleeHitbox.enabled = SurvivorCrocoCfg.Bind("00. General", "Extend Melee Hitbox", true, "Extends Acrid's melee hitbox so he can hit Vagrants while standing on top of them.").Value;
            BlightStack.enabled = SurvivorCrocoCfg.Bind("00. General", "Blight Duration Reset", true, "Blight stacks like Bleed.").Value;
            RemovePoisonDamageCap.enabled = SurvivorCrocoCfg.Bind("00. General", "Remove Poison Damage Cap", true, "Poison no longer has a hidden damage cap.").Value;
            BiggerLeapHitbox.enabled = SurvivorCrocoCfg.Bind("00. General", "Extend Leap Collision Box", true, "Acrid's Shift skills have a larger collision hitbox. Damage radius remains the same.").Value;
            ShiftAirControl.enabled = SurvivorCrocoCfg.Bind("00. General", "Leap Air Control", true, "Acrid's Shift skills gain increased air control at high move speeds.").Value;

            CrocoCore.Cfg.Stats.enabled = SurvivorCrocoCfg.Bind("01. Stats", "0 - Enable Stat Changes", true, "Overwrite Acrid's stats with the values in the config.").Value;
            CrocoCore.Cfg.Stats.health = SurvivorCrocoCfg.Bind("01. Stats", "Health", 160f, "Base health.").Value;
            CrocoCore.Cfg.Stats.damage = SurvivorCrocoCfg.Bind("01. Stats", "Damage", 12f, "Base damage. Vanilla is 15").Value;
            CrocoCore.Cfg.Stats.regen = SurvivorCrocoCfg.Bind("01. Stats", "Regen", 2.5f, "Base health regeneration. Affected by difficulty.").Value;
            CrocoCore.Cfg.Stats.armor = SurvivorCrocoCfg.Bind("01. Stats", "Armor", 20f, "Base armor.").Value;

            CrocoCore.Cfg.Regenerative.healFraction = SurvivorCrocoCfg.Bind("02. Regenerative", "Regenerative Heal Fraction", 0.1f, "How much Regenerative heals. Affected by difficulty.").Value;
            CrocoCore.Cfg.Regenerative.healDuration = SurvivorCrocoCfg.Bind("02. Regenerative", "Regenerative Heal Duration", 3f, "How long it takes for Regenerative to heal its full amount.").Value;

            CrocoCore.Cfg.Passives.baseDoTDuration = SurvivorCrocoCfg.Bind("03. Passives", "Base DoT Duration", 6f, "How long Poison and Blight last for.").Value;
            CrocoCore.Cfg.Passives.virulentDurationMult = SurvivorCrocoCfg.Bind("03. Passives", "Virulent Duration Multiplier", 1.8f, "How much to multiply DoT duration by when Virulent is selected.").Value;
            CrocoCore.Cfg.Passives.contagionSpreadRange = SurvivorCrocoCfg.Bind("03. Passives", "Contagion Spread Range", 30f, "How far Contagion can spread.").Value;

            CrocoCore.Cfg.Skills.ViciousWounds.baseDuration = SurvivorCrocoCfg.Bind("04. Primary - Vicious Wounds", "Base Duration", 1.2f, "Time it takes for each slash. Vanilla is 1.5").Value;
            CrocoCore.Cfg.Skills.ViciousWounds.damageCoefficient = SurvivorCrocoCfg.Bind("04. Primary - Vicious Wounds", "Damage Coefficient", 2f, "Skill damage.").Value;
            CrocoCore.Cfg.Skills.ViciousWounds.finisherDamageCoefficient = SurvivorCrocoCfg.Bind("04. Primary - Vicious Wounds", "Finisher Damage Coefficient", 4f, "Damage of the 3rd combo hit.").Value;

            CrocoCore.Cfg.Skills.Neurotoxin.damageCoefficient = SurvivorCrocoCfg.Bind("05. Secondary - Neurotoxin", "Damage Coefficient", 2.4f, "Skill damage.").Value;
            CrocoCore.Cfg.Skills.Neurotoxin.cooldown = SurvivorCrocoCfg.Bind("05. Secondary - Neurotoxin", "Cooldown", 2f, "Skill cooldown.").Value;

            CrocoCore.Cfg.Skills.Bite.damageCoefficient = SurvivorCrocoCfg.Bind("06. Secondary - Ravenous Bite", "Damage Coefficient", 3.6f, "Skill damage. Vanilla is 3.2").Value;
            CrocoCore.Cfg.Skills.Bite.cooldown = SurvivorCrocoCfg.Bind("06. Secondary - Ravenous Bite", "Cooldown", 2f, "Skill cooldown.").Value;
            CrocoCore.Cfg.Skills.Bite.healFractionOnKill = SurvivorCrocoCfg.Bind("06. Secondary - Ravenous Bite", "Heal on Kill Fraction", 0.08f, "How much HP to heal when killing with this skill.").Value;

            CrocoCore.Cfg.Skills.CausticLeap.cooldown = SurvivorCrocoCfg.Bind("07. Utility - Caustic Leap", "Cooldown", 6f, "Skill cooldown.").Value;
            CrocoCore.Cfg.Skills.CausticLeap.damageCoefficient = SurvivorCrocoCfg.Bind("07. Utility - Caustic Leap", "Damage Coefficient", 3.2f, "Skill damage.").Value;
            CrocoCore.Cfg.Skills.CausticLeap.acidProcCoefficient = SurvivorCrocoCfg.Bind("07. Utility - Caustic Leap", "Acid Proc Coefficient", 0.5f, "Affects the chance and power of item effects triggered by the acid puddle. Vanilla is 0.1").Value;

            CrocoCore.Cfg.Skills.FrenziedLeap.cooldown = SurvivorCrocoCfg.Bind("08. Utility - Frenzied Leap", "Cooldown", 6f, "Skill cooldown.").Value;
            CrocoCore.Cfg.Skills.FrenziedLeap.cooldownReduction = SurvivorCrocoCfg.Bind("08. Utility - Frenzied Leap", "Cooldown Reduction", 1f, "Amount of cooldown to refund per enemy hit.").Value;
            CrocoCore.Cfg.Skills.FrenziedLeap.damageCoefficient = SurvivorCrocoCfg.Bind("08. Utility - Frenzied Leap", "Damage Coefficient", 5.5f, "Skill damage.").Value;

            CrocoCore.Cfg.Skills.Epidemic.cooldown = SurvivorCrocoCfg.Bind("09. Special - Epidemic", "Cooldown", 10f, "Skill cooldown.").Value;
            CrocoCore.Cfg.Skills.Epidemic.baseTickCount = SurvivorCrocoCfg.Bind("09. Special - Epidemic", "Tick Count", 7, "Number of ticks that Epidemic hits for. Does not include the initial hit that applies it.").Value;
            CrocoCore.Cfg.Skills.Epidemic.damageCoefficient = SurvivorCrocoCfg.Bind("09. Special - Epidemic", "Damage Coefficient", 1f, "How much damage each tick of Epidemic deals.").Value;
            CrocoCore.Cfg.Skills.Epidemic.procCoefficient = SurvivorCrocoCfg.Bind("09. Special - Epidemic", "Proc Coefficient", 0.5f, "Affects the chance and power of item effects triggered by this skill.").Value;
            CrocoCore.Cfg.Skills.Epidemic.spreadRange = SurvivorCrocoCfg.Bind("09. Special - Epidemic", "Spread Range", 30f, "How far Epidemic can spread on each bounce.").Value;
            
        }*/
    }
}