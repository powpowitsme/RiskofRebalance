using R2API;
using RoR2;
using System;
using System.Collections.Generic;

namespace RiskofRebalance.Items
{
    public class BisonSteak
    {
        private readonly Dictionary<string, string> DefaultLanguage = new();
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
                ReplaceText();
            }
            catch (Exception e)
            {
                MainFile.logger.LogError(e.Message + " - " + e.StackTrace);
            }
        }

        private void ReplaceText()
        {
            ReplaceString("ITEM_FLATHEALTH_PICKUP", "Gain 8% max health.");
            ReplaceString("ITEM_FLATHEALTH_DESC", "Increases <style=cIsHealing>maximum health</style> by <style=cIsHealing>8%</style> <style=cStack>(+6% per stack)</style>.");
        }

        private void ReplaceString(string token, string newText)
        {
            DefaultLanguage[token] = Language.GetString(token);
            LanguageAPI.Add(token, newText);
        }
    }
}
