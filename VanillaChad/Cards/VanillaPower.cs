using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using UnboundLib;
using VanillaChad.MonoBehaviors;
using ModsPlus;
using ClassesManagerReborn.Util;

namespace ChadVanilla.Cards
{
    class VanPower : CustomEffectCard<VanPower_Mono>
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
    }
}

namespace VanillaChad.MonoBehaviors
{   
    [DisallowMultipleComponent]
    public class VanPower_Mono : CardEffect
    {
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
        public override void OnRevive() {
            GiveBuffs();
        }
    }
}
