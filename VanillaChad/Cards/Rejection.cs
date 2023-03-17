using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using ClassesManagerReborn.Util;
using System.Collections.Generic;

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
            Description = "Lose all vanilla chad cards, but gain great power in return",
            ModName     = ChadVanilla.ModInitials,
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Reject"),
            Rarity      = RarityUtils.GetRarity("Mythical"),
            Theme       = CardThemeColor.CardThemeColorType.MagicPink
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
        }
        public bool isVan(CardInfo card)
        {
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            foreach (var vc in vanillaCards) if (card.cardName.ToLower() == vc.cardName.ToLower())
                {
                    chadCards++;
                    return true;
                }
            return false;
        }
        public bool isVanChad(CardInfo card)
        {
            vccards = new[] {Chadious.card,VanPower.card,VanEnhan.card,VanThief.card,VanGains.card,Rejectio.card};
            foreach (var vc in vccards)
            {
                if (card.cardName.ToLower().Equals(vc.cardName.ToLower()))
                {
                    chadCards++;
                    return true;
                }
            }
            return false;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            ChadVanilla.instance.ExecuteAfterFrames(5, ()=>
            {
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                List<int> cardsOnPlayer = new List<int>();
                for (int i = 0; i < player.data.currentCards.Count; i++)
                {
                    var card = player.data.currentCards[i];
                    if(isVanChad(card))
                    {
                        cardsOnPlayer.Add(i);
                    }
                    if(isVan(card))
                    {
                        cardsOnPlayer.Add(i);
                    }
                }
                Buff.chadCards = chadCards;
                var removed = ModdingUtils.Utils.Cards.instance.RemoveCardsFromPlayer(player, cardsOnPlayer.ToArray());
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, removed);
                ChadVanilla.instance.ExecuteAfterFrames(20, () =>
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, Buff.card, false, null, 0, 0);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowCard(player, Buff.card);
                });
            });
        }
    }
}