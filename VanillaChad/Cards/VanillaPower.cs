using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using UnboundLib;
using ChadVanilla.Extensions;
using ChadVanilla.MonoBehaviors;
using ModsPlus;
using ClassesManagerReborn.Util;
using System.Collections;
using UnboundLib.GameModes;

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
            ModName     = ChadVanilla.ModInitials,
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Gladious"),
            Rarity      = CardInfo.Rarity.Rare,
            Theme       = CardThemeColor.CardThemeColorType.MagicPink,
            Stats = new []
            {
                new CardInfoStat
                {
                    amount = "+0.5%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "basic stats per vanilla card every point"
                }
            }
        };
    }
}

namespace ChadVanilla.MonoBehaviors
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
            double numboo = System.Math.Pow(1.005,vanPowers);
            float multiplier = (float)System.Math.Pow(numboo,vanCards);
            gun.damage *= multiplier;
            gunAmmo.reloadTimeMultiplier /= multiplier;
            gun.attackSpeed *= multiplier;
            gun.projectileSpeed *= multiplier;
            gun.attackSpeed /= multiplier;
            characterStats.health *= multiplier;
            characterStats.movementSpeed *= multiplier;
        }

        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            GiveBuffs();
            yield break;
        }
    }
}
