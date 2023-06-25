using RiskofRebalance.Items.Common;
using RiskofRebalance.Items.Void;
using RoR2;

namespace RiskofRebalance.Managers
{
    public class ItemManager
    {
        public static bool enabled = true;
        public static bool commonEnabled = true;
        public static bool uncommonEnabled = true;
        public static bool legendaryEnabled = true;
        public static bool bossEnabled = true;
        public static bool lunarEnabled = true;
        public static bool voidEnabled = true;
        public static bool equipmentEnabled = true;

        public static ItemDef[] changedItemPickups = new ItemDef[0];
        public static ItemDef[] changedItemDescs = new ItemDef[0];

        public static EquipmentDef[] changedEquipPickups = new EquipmentDef[0];
        public static EquipmentDef[] changedEquipDescs = new EquipmentDef[0];

        public ItemManager()
        {
            ModifyItemTokens();
            if (!enabled) return;
            ModifyCommon();
            ModifyUncommon();
            ModifyLegendary();
            ModifyVoid();
            ModifyBoss();
            ModifyLunar();
            ModifyEquipment();
        }

        private void ModifyCommon()
        {
            if (!commonEnabled) return;
            new BisonSteak();
            /*
            new StunGrenade(); 
            new StickyBomb();
            */
        }

        private void ModifyUncommon()
        {
            if (!uncommonEnabled) return;
            /*
            A little sneak peek for your troubles.
            new HuntersHarpoon();
            new Infusion();
            new OldWarStealthkit();
            new SquidPolyp();
            new LeptonDaisy();
            */
        }

        private void ModifyLegendary()
        {
            if (!legendaryEnabled) return;
        }

        private void ModifyVoid()
        {
            if (!voidEnabled) return;
            //new PlasmaShrimp();
            new Polylute();
            /*
            new BenthicBloom();
            new LostSeersLenses();
            new Needletick();
            new SingularityBand();
            new Tentabauble();
            */
        }

        private void ModifyBoss()
        {
            if (!bossEnabled) return;
            /*
            new DefenseNucleus();
            new EmpathyCores();
            new GenesisLoop();
            new HalcyonSeed();

            */
        }

        private void ModifyLunar()
        {
            if (!lunarEnabled) return;
            /*
            new Corpsebloom();
            new StoneFluxPauldron();
            new LightFluxPauldron();
            new EulogyZero();
            new BeadsOfFealty();
            */
        }

        private void ModifyEquipment()
        {
            if (!equipmentEnabled) return;
            /*
            new HerBitingEmbrace();
            new HisReassurance();
            new IfritsDistinction();
            new NkuhanasRetort();
            new SharedDesign();
            new SilenceBetweenTwoStrikes();
            new SpectralCirclet();
            new GooboJr();
            */
        }

        private void ModifyItemTokens()
        {
            On.RoR2.ItemCatalog.Init += (orig) =>
            {
                orig();

                ModifyItemDefActions?.Invoke();

                foreach (ItemDef item in changedItemPickups)
                {
                    item.pickupToken += "_RISKOFREBALANCE";
                }
                foreach (ItemDef item in changedItemDescs)
                {
                    item.descriptionToken += "_RISKOFREBALANCE";
                }
                foreach (EquipmentDef item in changedEquipPickups)
                {
                    item.pickupToken += "_RISKOFREBALANCE";
                }
                foreach (EquipmentDef item in changedEquipDescs)
                {
                    item.descriptionToken += "_RISKOFREBALANCE";
                }
            };
        }

        public delegate void ModifyItemDef();
        public static ModifyItemDef ModifyItemDefActions;

        public static EquipmentDef LoadEquipmentDef(string equipmentname)
        {
            return LegacyResourcesAPI.Load<EquipmentDef>("equipmentdefs/" + equipmentname);
        }

        public static void ChangeEquipmentCooldown(EquipmentDef ed, float cooldown)
        {
            ed.cooldown = cooldown;
        }
    }
}