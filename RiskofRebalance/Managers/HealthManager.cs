using RoR2;
using RoR2.CharacterAI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskofRebalance.Managers
{
    public class HealthManager
    {
        public class TakeDamage
        {
            public delegate void OnPercentHpLost(DamageInfo damageInfo, HealthComponent self, Inventory inventory, float percentHpLost);
            public static OnPercentHpLost OnPercentHpLostActions;

            public delegate void OnHpLostAttacker(DamageInfo damageInfo, HealthComponent self, CharacterBody attackerBody, Inventory inventory, float hpLost);
            public static OnHpLostAttacker OnHpLostAttackerActions;

            public delegate void ModifyInitialDamage(DamageInfo damageInfo, HealthComponent self, CharacterBody attackerBody);
            public static ModifyInitialDamage ModifyInitialDamageActions;

            public delegate void ModifyInitialDamageInventory(DamageInfo damageInfo, HealthComponent self, CharacterBody attackerBody, Inventory attackerInventory);
            public static ModifyInitialDamageInventory ModifyInitialDamageInventoryActions;

            public delegate void OnDamageTaken(DamageInfo damageInfo, HealthComponent self);
            public static OnDamageTaken OnDamageTakenActions;

            public delegate void OnDamageTakenAttacker(DamageInfo damageInfo, HealthComponent self, CharacterBody attackerBody);
            public static OnDamageTakenAttacker OnDamageTakenAttackerActions;

            public delegate void TakeDamageEnd(DamageInfo damageInfo, HealthComponent self);
            public static TakeDamageEnd TakeDamageEndActions;

            public static List<BodyIndex> distractOnHitBodies = new();
#pragma warning disable IDE0060 // Remove unused parameter
            public static void DistractOnHit(DamageInfo damageInfo, HealthComponent self, CharacterBody attackerBody)
#pragma warning restore IDE0060 // Remove unused parameter
            {
                //Based on https://github.com/DestroyedClone/PoseHelper/blob/master/HighPriorityAggroTest/HPATPlugin.cs
                if (!self.body.isChampion && self.body.master && self.body.master.aiComponents.Length > 0 && distractOnHitBodies.Contains(attackerBody.bodyIndex))
                {
                    foreach (BaseAI ai in self.body.master.aiComponents)
                    {
                        ai.currentEnemy.gameObject = attackerBody.gameObject;
                        ai.currentEnemy.bestHurtBox = attackerBody.mainHurtBox;
                        ai.enemyAttention = ai.enemyAttentionDuration;
                        ai.targetRefreshTimer = 5f;
                        ai.BeginSkillDriver(ai.EvaluateSkillDrivers());
                    }
                }
            }

            public static void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
            {
                float oldHP = self.combinedHealth;
                CharacterBody attackerBody = null;
                Inventory attackerInventory = null;
                if (damageInfo.attacker)
                {
                    attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
                    if (attackerBody)
                    {
                        ModifyInitialDamageActions?.Invoke(damageInfo, self, attackerBody);
                        attackerInventory = attackerBody.inventory;
                        if (attackerInventory)
                        {
                            ModifyInitialDamageInventoryActions?.Invoke(damageInfo, self, attackerBody, attackerInventory);
                        }
                    }
                }

                orig(self, damageInfo);

                if (NetworkServer.active && !damageInfo.rejected)
                {
                    if (self.alive)
                    {
                        OnDamageTakenActions?.Invoke(damageInfo, self);

                        if (damageInfo.attacker)
                        {
                            if (attackerBody)
                            {
                                OnDamageTakenAttackerActions?.Invoke(damageInfo, self, attackerBody);
                            }
                        }

                        Inventory inventory = self.body.inventory;
                        if (inventory)
                        {
                            float totalHPLost = oldHP - self.combinedHealth;
                            if (totalHPLost > 0f)
                            {
                                if (attackerBody)
                                {
                                    OnHpLostAttackerActions?.Invoke(damageInfo, self, attackerBody, inventory, totalHPLost);
                                }
                                float percentHPLost = totalHPLost / self.fullCombinedHealth * 100f;
                                OnPercentHpLostActions?.Invoke(damageInfo, self, inventory, percentHPLost);
                            }
                        }
                        TakeDamageEndActions?.Invoke(damageInfo, self);
                    }
                }
            }
        }
        public class OnCharacterDeath
        {
            public delegate void OnCharacterDeathInventory(GlobalEventManager self, DamageReport damageReport, CharacterBody attackerBody, Inventory attackerInventory, CharacterBody victimBody);
            public static OnCharacterDeathInventory OnCharacterDeathInventoryActions;

            public static void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
            {
                orig(self, damageReport);

                if (NetworkServer.active)
                {
                    CharacterBody attackerBody = damageReport.attackerBody;
                    CharacterMaster attackerMaster = damageReport.attackerMaster;
                    //TeamIndex attackerTeamIndex = damageReport.attackerTeamIndex;
                    DamageInfo damageInfo = damageReport.damageInfo;
                    //GameObject victimObject = damageReport.victim.gameObject;
                    CharacterBody victimBody = damageReport.victimBody;
                    Inventory attackerInventory = attackerMaster ? attackerMaster.inventory : null;

                    if (attackerBody && attackerMaster)
                    {
                        if (victimBody)
                        {
                            /*
                            if (Crowbar.enabled && Crowbar.crowbarManager)
                            {
                                Crowbar.crowbarManager.Remove(victimBody.healthComponent);
                            }
                            */

                            if (attackerInventory && OnCharacterDeathInventoryActions != null)
                            {
                                OnCharacterDeathInventoryActions.Invoke(self, damageReport, attackerBody, attackerInventory, victimBody);
                            }
                            /*
                            if (AssistManager.initialized && RiskyMod.assistManager)
                            {
                                //On-death is handled by assist manager to prevent having a bunch of duplicated code.
                                //Need to add an assist here since it's called before OnHitEnemy.
                                RiskyMod.assistManager.AddAssist(attackerBody, victimBody, AssistManager.assistLength);
                                if ((damageInfo.damageType & DamageType.ResetCooldownsOnKill) > DamageType.Generic)
                                {
                                    RiskyMod.assistManager.AddDirectAssist(attackerBody, victimBody, BanditSpecialGracePeriod.GetDuration(attackerBody), AssistManager.DirectAssistType.ResetCooldowns);
                                }
                                if ((damageInfo.damageType & DamageType.GiveSkullOnKill) > DamageType.Generic)
                                {
                                    RiskyMod.assistManager.AddDirectAssist(attackerBody, victimBody, BanditSpecialGracePeriod.GetDuration(attackerBody), AssistManager.DirectAssistType.BanditSkull);
                                }
                                if (damageInfo.HasModdedDamageType(SharedDamageTypes.CrocoBiteHealOnKill))
                                {
                                    RiskyMod.assistManager.AddDirectAssist(attackerBody, victimBody, AssistManager.directAssistLength, AssistManager.DirectAssistType.CrocoBiteHealOnKill);
                                }
                                RiskyMod.assistManager.TriggerAssists(victimBody, attackerBody, damageInfo);
                            }
                            */
                        }
                    }
                }
            }
        }
        public class HealthComponent_UpdateLastHitTime
        {
            public delegate void UpdateLastHitTimeDelegate(HealthComponent self, float damageValue, Vector3 damagePosition, bool damageIsSilent, GameObject attacker);
            public static UpdateLastHitTimeDelegate UpdateLastHitTimeActions;

            public delegate void UpdateLastHitTimeInventoryDelegate(HealthComponent self, float damageValue, Vector3 damagePosition, bool damageIsSilent, GameObject attacker, Inventory inventory);
            public static UpdateLastHitTimeInventoryDelegate UpdateLastHitTimeInventoryActions;

            public static void UpdateLastHitTime(On.RoR2.HealthComponent.orig_UpdateLastHitTime orig, HealthComponent self, float damageValue, Vector3 damagePosition, bool damageIsSilent, GameObject attacker)
            {
                orig(self, damageValue, damagePosition, damageIsSilent, attacker);
                if (NetworkServer.active && self.body && damageValue > 0f)
                {
                    UpdateLastHitTimeActions?.Invoke(self, damageValue, damagePosition, damageIsSilent, attacker);

                    if (self.body.inventory)
                    {
                        UpdateLastHitTimeInventoryActions?.Invoke(self, damageValue, damagePosition, damageIsSilent, attacker, self.body.inventory);
                    }
                }
            }
        }

    }

}