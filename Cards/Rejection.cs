using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using ClassesManagerReborn.Util;
using System.Collections.Generic;
using System.Linq;

namespace ChadVanilla.Cards
{
    public class Rejectio : SimpleCard
    {
        internal static CardInfo card = null;
        public static CardInfo[] vccards;
        
        int chadCards = 0;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Rejection",
            Description = "Lose all vanilla chad cards and vanilla cards, but gain great power in return",
            ModName     = ChadVanilla.ModInitials,
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Reject"),
            Rarity      = RarityUtils.GetRarity("Mythical"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
        }
        public bool isVan(CardInfo card)
        {
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            foreach (var vc in vanillaCards) if (card.cardName.ToLower() == vc.cardName.ToLower()) return true;
            return false;
        }
        public bool isVanChad(CardInfo card, int num)
        {
            vccards = new[] {Chadious.card,VanPower.card,VanEnhan.card,VanThief.card,VanGains.card};
            foreach (var vc in vccards)
            {
                if (card.cardName.ToLower().Equals(vc.cardName.ToLower()))
                {
                    chadCards+=num;
                    return true;
                }
            }
            return false;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            foreach(var card in player.data.currentCards) isVanChad(card,1);
            float mult = (float)System.Math.Pow(1.2,(double)chadCards);
            cardInfo.cardStats = new []
            {
                new CardInfoStat
                {
                    amount = "+"+((mult-1.0f)*100)+"%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "stats"
                }
            };
            gun.damage*=mult;
            gun.attackSpeed/=mult;
            gunAmmo.reloadTimeMultiplier/=mult;
            player.data.maxHealth*=mult;
            characterStats.movementSpeed*=mult;
            characterStats.jump*=mult;
            gunAmmo.maxAmmo+=chadCards;
            gun.reflects+=chadCards;
            ChadVanilla.instance.ExecuteAfterFrames(20, ()=>
            {
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                foreach (var vc in vanillaCards)
                {
                    RarityUtils.AjustCardRarityModifier(vc, 0.0f, -9.0f);
                }
                vccards = new[] {Chadious.card,VanPower.card,VanEnhan.card,VanThief.card,VanGains.card};
                foreach (var vc in vccards)
                {
                    RarityUtils.AjustCardRarityModifier(vc, 0.0f, -999.0f);
                }
                List<int> cardsOnPlayer = new List<int>();
                for (int i = 0; i < player.data.currentCards.Count; i++)
                {
                    var card = player.data.currentCards[i];
                    if(isVanChad(card,0))
                    {
                        cardsOnPlayer.Add(i);
                    }
                }
                var removed = ModdingUtils.Utils.Cards.instance.RemoveCardsFromPlayer(player, cardsOnPlayer.ToArray());
                //ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, removed);
            });
        }
    }
}