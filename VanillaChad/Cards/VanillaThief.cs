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
    class VanThief : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>();
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Vanilla Thief",
            Description = "Steal some vanilla cards from other players",
            ModName     = "CHAD",
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Thiefy"),
            Rarity      = RarityUtils.GetRarity("Epic"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite,
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if(player.data.view.IsMine)
            {
                int cardsFound = 0;
                List<CardInfo> vanilIndeces = new List<CardInfo>();
                List<Player> playersCoris = new List<Player>();
                List<Player> otherPlayers = PlayerManager.instance.players.Where((pl) => pl.playerID != player.playerID).ToList();
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                foreach (var perp in otherPlayers)
                {
                    foreach (var cood in perp.data.currentCards)
                    {
                        foreach (var vc in vanillaCards)
                        {
                            if(cood.cardName.ToLower() == vc.cardName.ToLower())
                            {
                                vanilIndeces.Add(cood);
                                playersCoris.Add(perp);
                                cardsFound++;
                            }
                        }
                    }
                }
                List<CardInfo> cardsToAdd = new List<CardInfo>();
                List<int> rng = new List<int>();
                if(cardsFound >= 2) {
                    rng.Add(Random.Range(0,cardsFound-1));
                    rng.Add(Random.Range(0,cardsFound-1));
                    while(rng[1].Equals(rng[0])) rng[1]= (UnityEngine.Random.Range(0,cardsFound-1));
                    var pA = playersCoris[rng[0]];
                    var cA = vanilIndeces[rng[0]];
                    var pB = playersCoris[rng[1]];
                    var cB = vanilIndeces[rng[1]];
                    CardInfo[] coooods = new CardInfo[2];
                    coooods[0] = cA;
                    coooods[1] = cB;
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(pA,pA.data.currentCards.IndexOf(cA));
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(pB,pB.data.currentCards.IndexOf(cB));
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, coooods, false, null, null, null, true);
                } else if (cardsFound == 1) {
                    var pA = playersCoris[0];
                    var cA = vanilIndeces[0];
                    CardInfo[] coooods = new CardInfo[1];
                    coooods[0] = cA;
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(pA,pA.data.currentCards.IndexOf(cA));
                    ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, coooods, false, null, null, null, true);
                }
            }
        }
        
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }
    }
}
