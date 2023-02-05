using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ChadVanilla.IHook;
using UnboundLib;
using VanillaChad.MonoBehaviors;
using ModsPlus;
using ClassesManagerReborn.Util;

namespace ChadVanilla.Cards
{
    class VanPower : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Vanilla Power",
            Description = "Gain compounding stats for every vanilla card owned",
            ModName     = "CHAD",
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Gladious"),
            Rarity      = RarityUtils.GetRarity("Epic"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new []
            {
                new CardInfoStat
                {
                    amount = "+5%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "basic stats per"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<VanillaPower_Mono>();
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetOrAddComponent<VanillaPower_Mono>());
        }
    }
}

namespace VanillaChad.MonoBehaviors
{   
    [DisallowMultipleComponent]
    public class VanillaPower_Mono : PlayerHook, IPointStartHookHandler
    {
        public int countVanilla() {
            double vanCards = 0.0;
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                foreach (var vc in vanillaCards)
                {
                    if (player.data.currentCards[i].cardName == vc.cardName)
                    {
                        vanCards++;
                    }
                }
            }
            return (int)vanCards;
        }
        private void GiveBuffs()
        {
            double vanCards = 0.0;
            double vanPowers = 0.0;
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                foreach (var vc in vanillaCards)
                {
                    if (player.data.currentCards[i].cardName == vc.cardName)
                    {
                        vanCards++;
                    }
                }
                if(player.data.currentCards[i].cardName.ToLower() == "Vanilla Power".ToLower())
                {
                    vanPowers++;
                }
            }
            float multiplier = (float)System.Math.Pow(1.05,vanCards)*(float)vanPowers;
            StatChanges stuffs = new StatChanges() {
                Damage = multiplier,
                AttackSpeed = 1/multiplier,
                MaxHealth = multiplier,
                MovementSpeed = multiplier/2,
                JumpHeight = multiplier/2
            };
            StatManager.Apply(player, stuffs);  
        }

        public void OnPointStart() {
            GiveBuffs();
        }
        public void OnDeath() {
            GiveBuffs();
        }
    }
}
