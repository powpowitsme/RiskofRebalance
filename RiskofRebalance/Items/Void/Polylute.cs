using R2API;
using RoR2;
using System;
using System.Collections.Generic;

namespace RiskofRebalance.Items.Void
{
    public class Polylute
    {
        public static bool enabled = true;
        private readonly Dictionary<string, string> DefaultLanguage = new();
        public void Awake()
        {
            try
            {
                RecalculateStatsAPI.GetStatCoefficients += delegate (CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
                {
                    if (!sender.inventory) return;
                    var amount = sender.inventory.GetItemCount(DLC1Content.Items.ChainLightningVoid);
                    args.damageMultAdd -= 0.1f;
                };
                ReplaceText();
            }
            catch (Exception e)
            {
                RiskofRebalance.logger.LogError(e.Message + " - " + e.StackTrace);
            }
        }

        private void ReplaceText()
        {
            ReplaceString("ITEM_CHAINLIGHTNINGVOID_PICKUP", "Gain 8% max health.");
            ReplaceString("ITEM_CHAINLIGHTNINGVOID_DESC", "<style=cIsDamage>25%</style> chance to fire <style=cIsDamage>lightning</style> for <style=cIsDamage>50%</style> TOTAL damage up to <style=cIsDamage>3 <style=cStack>(+3 per stack)</style></style> times. <style=cIsVoid>Corrupts all Ukuleles</style>.");
        }

        private void ReplaceString(string token, string newText)
        {
            DefaultLanguage[token] = Language.GetString(token);
            LanguageAPI.Add(token, newText);
        }
    }
}
