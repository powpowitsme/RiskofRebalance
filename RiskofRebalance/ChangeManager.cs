using RoR2;
using R2API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace RiskofRebalance
{
    public class ChangeManager
    {
        public class Definitions
        {
            public EquipmentDef Equipment;
            public BuffDef Buff;
            public DotController.DotDef Dot;
            public SurvivorDef Survivor;
        }
        public static class Changes
        {
            public static void UpdateItem(ItemDef itemDef, ItemTier tier = ItemTier.NoTier, ItemTag[] tags = null,
            string name = "", string pickup = "", string description = "", string lore = "", bool canRemove = true)
            {
                if (tier != ItemTier.NoTier)
                {
                    itemDef.tier = tier;
                }
                string Prefix = "RoR2";
                if (name.Length > 0)
                {
                    string nameToken = Prefix + itemDef.nameToken;
                    itemDef.nameToken = nameToken;
                    LanguageAPI.Add(nameToken, name);
                }
                if (pickup.Length > 0)
                {
                    string pickupToken = Prefix + itemDef.pickupToken;
                    itemDef.pickupToken = pickupToken;
                    LanguageAPI.Add(pickupToken, pickup);
                    Log.Message(itemDef.pickupToken);
                }
                if (description.Length > 0)
                {
                    string descriptionToken = Prefix + itemDef.descriptionToken;
                    itemDef.descriptionToken = descriptionToken;
                    LanguageAPI.Add(descriptionToken, description);
                }
                if (lore.Length > 0)
                {
                    string loreToken = Prefix + itemDef.loreToken;
                    itemDef.loreToken = loreToken;
                    LanguageAPI.Add(loreToken, lore);
                }
                if (tags != null)
                {
                    itemDef.tags = tags;
                }

                if (canRemove == false)
                {
                    itemDef.canRemove = false;
                }
                else
                {
                    itemDef.canRemove = true;
                }
            }
        }

        public void ChangeItemTokens()
        {
            Log.Info("Updating item tokens.");
            Changes.UpdateItem(RoR2Content.Items.FlatHealth,
                pickup: "Gain 8% max health.",
                description: "Increases <style=cIsHealing>maximum health</style> by <style=cIsHealing>8%</style> <style=cStack>(+6% per stack)</style>.");
        }
    }
}
