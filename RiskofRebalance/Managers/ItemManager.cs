using RiskofRebalance.Items;
using RoR2;

namespace RiskofRebalance.Managers
{
    public class ItemManager
    {
        public static bool enabled = true;
        public static bool uncommonEnabled = true;
        public static bool commonEnabled = true;
        public static bool legendaryEnabled = true;
        public static bool bossEnabled = true;
        public static bool lunarEnabled = true;
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
        }

        private void ModifyUncommon()
        {
            if (!uncommonEnabled) return;
        }

        private void ModifyLegendary()
        {
            if (!legendaryEnabled) return;
        }

        private void ModifyVoid()
        {
            new Polylute();
        }

        private void ModifyBoss()
        {
            if (!bossEnabled) return;
        }

        private void ModifyLunar()
        {
            if (!lunarEnabled) return;
        }

        private void ModifyEquipment()
        {
            if (!equipmentEnabled) return;
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