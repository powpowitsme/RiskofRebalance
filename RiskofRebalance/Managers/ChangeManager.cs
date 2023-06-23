using BepInEx.Configuration;
using R2API;
using RoR2;
using RoR2.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RiskofRebalance.Managers
{
    public class ChangeManager
    {

        public static bool ReplaceSkillDef(SkillFamily skillFamily, SkillDef targetSkill, SkillDef newSkill)
        {
            bool successfullyReplaced = false;

            if (skillFamily.variants != null && targetSkill != null && newSkill != null)
            {
                for (int i = 0; i < skillFamily.variants.Length; i++)
                {
                    if (skillFamily.variants[i].skillDef == targetSkill)
                    {
                        skillFamily.variants[i].skillDef = newSkill;
                        successfullyReplaced = true;
                        break;
                    }
                }
            }

            if (!successfullyReplaced)
            {
                Debug.LogError("RiskyMod: Could not replace TargetSkill " + targetSkill);
            }
            return successfullyReplaced;
        }

        //Taken from https://github.com/ToastedOven/CustomEmotesAPI/blob/main/CustomEmotesAPI/CustomEmotesAPI/CustomEmotesAPI.cs
        public static bool GetKeyPressed(ConfigEntry<KeyboardShortcut> entry)
        {
            foreach (var item in entry.Value.Modifiers)
            {
                if (!Input.GetKey(item))
                {
                    return false;
                }
            }
            return Input.GetKeyDown(entry.Value.MainKey);
        }

        public static void FixSkillName(SkillDef skillDef)
        {
            (skillDef as UnityEngine.Object).name = skillDef.skillName;// "RiskyMod_" + 
        }

        public static BuffDef CreateBuffDef(string name, bool canStack, bool isCooldown, bool isDebuff, Color color, Sprite iconSprite)
        {
            BuffDef bd = ScriptableObject.CreateInstance<BuffDef>();
            bd.name = name;
            bd.canStack = canStack;
            bd.isCooldown = isCooldown;
            bd.isDebuff = isDebuff;
            bd.buffColor = color;
            bd.iconSprite = iconSprite;

            (bd as UnityEngine.Object).name = bd.name;
            return bd;
        }

        public static bool IsLocalUser(GameObject playerObject)
        {
            foreach (LocalUser user in LocalUserManager.readOnlyLocalUsersList)
            {
                if (playerObject == user.cachedBodyObject)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsLocalUser(CharacterBody playerBody)
        {
            foreach (LocalUser user in LocalUserManager.readOnlyLocalUsersList)
            {
                if (playerBody == user.cachedBody)
                {
                    return true;
                }
            }

            return false;
        }

        public static void RemoveItemTag(ItemDef itemDef, ItemTag tag)
        {
            if (itemDef.ContainsTag(tag))
            {
                List<ItemTag> tags = itemDef.tags.ToList();
                tags.Remove(tag);
                itemDef.tags = tags.ToArray();

                ItemIndex index = itemDef.itemIndex;
                if (index != ItemIndex.None && ItemCatalog.itemIndicesByTag != null && ItemCatalog.itemIndicesByTag[(int)tag] != null)
                {
                    List<ItemIndex> itemList = ItemCatalog.itemIndicesByTag[(int)tag].ToList();
                    if (itemList.Contains(index))
                    {
                        itemList.Remove(index);
                        ItemCatalog.itemIndicesByTag[(int)tag] = itemList.ToArray();
                    }
                }
            }
        }

        public static void AddItemTag(ItemDef itemDef, ItemTag tag)
        {
            if (!itemDef.ContainsTag(tag))
            {
                List<ItemTag> tags = itemDef.tags.ToList();
                tags.Add(tag);
                itemDef.tags = tags.ToArray();

                ItemIndex index = itemDef.itemIndex;
                if (index != ItemIndex.None && ItemCatalog.itemIndicesByTag != null)
                {
                    List<ItemIndex> itemList = ItemCatalog.itemIndicesByTag[(int)tag].ToList();
                    if (!itemList.Contains(index))
                    {
                        itemList.Add(index);
                        ItemCatalog.itemIndicesByTag[(int)tag] = itemList.ToArray();
                    }
                }
            }
        }

        //Embarassing code, there has to be a better way.
        public static string FloatToString(float f)
        {
            int whole = Mathf.FloorToInt(f);
            int dec = Mathf.FloorToInt((f - whole) * 100f);
            return whole + "." + dec;
        }

        public class GetStatCoefficients
        {
            public delegate void HandleStatsInventory(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args, Inventory inventory);
            public static HandleStatsInventory HandleStatsInventoryActions;

            public static void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
            {
                if (sender.inventory && HandleStatsInventoryActions != null)
                {
                    HandleStatsInventoryActions.Invoke(sender, args, sender.inventory);
                }
            }
        }

        public class RecalculateStats
        {
            public delegate void HandleRecalculateStats(CharacterBody self);
            public static HandleRecalculateStats HandleRecalculateStatsActions;

            public delegate void HandleRecalculateStatsInventory(CharacterBody self, Inventory inventory);
            public static HandleRecalculateStatsInventory HandleRecalculateStatsInventoryActions;

            public static void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
            {
                orig(self);
                HandleRecalculateStatsActions?.Invoke(self);
                if (self.inventory && HandleRecalculateStatsInventoryActions != null)
                {
                    HandleRecalculateStatsInventoryActions.Invoke(self, self.inventory);
                }
            }
        }
    }
}