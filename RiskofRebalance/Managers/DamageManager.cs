using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskofRebalance.Managers
{
    public class DamageManager
    {
        public delegate void OnHitNoAttacker(DamageInfo damageInfo, CharacterBody victimBody);
        public static OnHitNoAttacker OnHitNoAttackerActions;

        public delegate void OnHitAttacker(DamageInfo damageInfo, CharacterBody victimBody, CharacterBody attackerBody);
        public static OnHitAttacker OnHitAttackerActions;

        public delegate void OnHitAttackerInventory(DamageInfo damageInfo, CharacterBody victimBody, CharacterBody attackerBody, Inventory attackerInventory);
        public static OnHitAttackerInventory OnHitAttackerInventoryActions;

        public delegate void OnHitAllDelegate(GlobalEventManager self, DamageInfo damageInfo, GameObject hitObject);
        public static OnHitAllDelegate HandleOnHitAllActions;

        public class OnHitEnemy
        {
            public static void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, UnityEngine.GameObject victim)
            {
                CharacterBody attackerBody = null;
                CharacterBody victimBody = null;
                Inventory attackerInventory = null;

                bool validDamage = NetworkServer.active && damageInfo.procCoefficient > 0f && !damageInfo.rejected;
                ///bool assistsEnabled = AssistManager.initialized && RiskofRebalance.assistManager;

                if (validDamage)
                {
                    if (damageInfo.attacker)
                    {
                        attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
                        victimBody = victim ? victim.GetComponent<CharacterBody>() : null;

                        if (attackerBody)
                        {
                            attackerInventory = attackerBody.inventory;
                            if (attackerInventory)
                            {
                                /*
                                if (assistsEnabled)
                                {
                                    RiskofRebalance.assistManager.AddAssist(attackerBody, victimBody, AssistManager.assistLength);
                                }
                                */
                            }
                        }

                        //Run this before triggering on-hit procs so that procs don't kill enemies before this triggers.
                        /*
                        if (assistsEnabled)
                        {
                            if ((damageInfo.damageType & DamageType.ResetCooldownsOnKill) > DamageType.Generic)
                            {
                                RiskofRebalance.assistManager.AddDirectAssist(attackerBody, victimBody, BanditSpecialGracePeriod.GetDuration(damageInfo.attacker), AssistManager.DirectAssistType.ResetCooldowns);
                            }
                            if ((damageInfo.damageType & DamageType.GiveSkullOnKill) > DamageType.Generic)
                            {
                                RiskofRebalance.assistManager.AddDirectAssist(attackerBody, victimBody, BanditSpecialGracePeriod.GetDuration(damageInfo.attacker), AssistManager.DirectAssistType.BanditSkull);
                            }
                            if (damageInfo.HasModdedDamageType(SharedDamageTypes.CrocoBiteHealOnKill))
                            {
                                RiskofRebalance.assistManager.AddDirectAssist(attackerBody, victimBody, AssistManager.directAssistLength, AssistManager.DirectAssistType.CrocoBiteHealOnKill);
                            }
                        }
                        */
                    }
                }

                orig(self, damageInfo, victim);

                if (validDamage)
                {
                    if (victimBody)
                    {
                        OnHitNoAttackerActions?.Invoke(damageInfo, victimBody);

                        if (damageInfo.attacker && attackerBody)
                        {
                            OnHitAttackerActions?.Invoke(damageInfo, victimBody, attackerBody);
                            if (attackerInventory && OnHitAttackerInventoryActions != null) OnHitAttackerInventoryActions.Invoke(damageInfo, victimBody, attackerBody, attackerInventory);
                        }
                    }
                }
            }
        }

        public class OnHitAll
        {
            public static void GlobalEventManager_OnHitAll(On.RoR2.GlobalEventManager.orig_OnHitAll orig, GlobalEventManager self, DamageInfo damageInfo, GameObject hitObject)
            {
                orig(self, damageInfo, hitObject);
                if (!NetworkServer.active || damageInfo.procCoefficient == 0f || damageInfo.rejected)
                {
                    return;
                }
                HandleOnHitAllActions?.Invoke(self, damageInfo, hitObject);
            }
        }
    }
}