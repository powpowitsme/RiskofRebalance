using Mono.Cecil.Cil;
using MonoMod.Cil;
using R2API;
using RiskofRebalance.Managers;
using RoR2;

namespace RiskofRebalance.Items.Common
{
    public class BisonSteak
    {
        public static bool enabled = true;
        public BisonSteak()
        {
            if (!enabled) return;

            ItemManager.ModifyItemDefActions += ModifyItem;

            //Remove Vanilla Effect
            IL.RoR2.CharacterBody.RecalculateStats += (il) =>
            {
                ILCursor c = new(il);
                if (c.TryGotoNext(
                     x => x.MatchLdsfld(typeof(RoR2Content.Items), "FlatHealth")
                    ))
                {
                    c.Remove();
                    c.Emit<RiskofRebalance>(OpCodes.Ldsfld, nameof(RiskofRebalance.emptyItemDef));
                }
                else
                {
                    UnityEngine.Debug.LogError("Failed to hook Bison Steak! (Risk of Rebalance)");
                }
            };
            ChangeManager.GetStatCoefficients.HandleStatsInventoryActions += HandleStatsInventory;

        }
        private static void ModifyItem()
        {
            HG.ArrayUtils.ArrayAppend(ref ItemManager.changedItemPickups, RoR2Content.Items.FlatHealth);
            HG.ArrayUtils.ArrayAppend(ref ItemManager.changedItemDescs, RoR2Content.Items.FlatHealth);
            Managers.ChangeManager.RemoveItemTag(RoR2Content.Items.FlatHealth, ItemTag.OnKillEffect);
        }
        private static void HandleStatsInventory(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args, Inventory inventory)
        {
            int steakCount = sender.inventory.GetItemCount(RoR2Content.Items.FlatHealth);
            if (steakCount > 0)
            {
                args.baseHealthAdd += 0.08f + 0.06f * (steakCount - 1);
            }
        }

    }
}
