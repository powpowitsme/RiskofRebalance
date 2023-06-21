using R2API;
using RoR2;
using System;
using System.Collections.Generic;

namespace RiskofRebalance.Items
{
    public class BisonSteak
    {
        public void Awake()
        {
            try
            {
                RecalculateStatsAPI.GetStatCoefficients += delegate (CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
                {
                    if (!sender.inventory) return;
                    var amount = sender.inventory.GetItemCount(RoR2Content.Items.FlatHealth);
                    args.baseHealthAdd -= amount * 25f;
                    args.healthMultAdd += 0.08f + 0.06f * (amount - 1);
                };
            }
            catch (Exception e)
            {
                MainFile.logger.LogError(e.Message + " - " + e.StackTrace);
            }
        }
    }
}
