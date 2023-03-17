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
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Vanilla Thief",
            Description = "Attempt to steal some vanilla cards from other players",
            ModName     = ChadVanilla.ModInitials,
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Thiefy"),
            Rarity      = CardInfo.Rarity.Rare,
            Theme       = CardThemeColor.CardThemeColorType.MagicPink
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
        }
        public bool isVan(CardInfo card)
        {
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            foreach (var vc in vanillaCards) if (card.cardName.ToLower().Equals(vc.cardName.ToLower())) return true;
            return false;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ChadVanilla.instance.ExecuteAfterFrames(20, () =>
            {
                int cardsToFind = 2;
                int cardsFound = 0;
                Dictionary<Player, List<int>> vanilIndeces = new Dictionary<Player, List<int>>();
                List<Player> allyPlayers = PlayerManager.instance.players.Where((pl) => pl.playerID != player.playerID && pl.teamID == player.teamID).ToList();
                List<Player> enemyPlayers = PlayerManager.instance.players.Where((pl) => pl.playerID != player.playerID && pl.teamID != player.teamID).ToList();
                List<Player> otherPlayers = PlayerManager.instance.players.Where((pl) => pl.playerID != player.playerID).ToList();
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                foreach (var person in otherPlayers.OrderBy((p1) => {if (enemyPlayers.Contains(p1)) {return 0; } return 1; }).ToArray())
                {
                    vanilIndeces.Add(person, (from index in Enumerable.Range(0, person.data.currentCards.Count()) where isVan(person.data.currentCards[index]) select index).ToList());
                    cardsFound += vanilIndeces[person].Count;
                    if(cardsFound >= cardsToFind)
                    {
                        break;
                    }
                }
                List<CardInfo> cardsToAdd = new List<CardInfo>();
                Dictionary<Player, List<int>> removedCards= vanilIndeces.ToDictionary((person) => person.Key, (person) => new List<int>());

                while (cardsToAdd.Count < cardsToFind)
                {
                    Player selectedPlayer;
                    if(vanilIndeces.Keys.Intersect(enemyPlayers).Count() > 0)
                    {
                        var options = vanilIndeces.Keys.Intersect(enemyPlayers).ToArray();
                        selectedPlayer = options[UnityEngine.Random.Range(0, options.Count())];
                        
                        var randomIndex = vanilIndeces[selectedPlayer][UnityEngine.Random.Range(0, vanilIndeces[selectedPlayer].Count())];
                        vanilIndeces[selectedPlayer].Remove(randomIndex);

                        var cord = selectedPlayer.data.currentCards[randomIndex];

                        cardsToAdd.Add(cord);
                        removedCards[selectedPlayer].Add(randomIndex);

                        if (!(vanilIndeces[selectedPlayer].Count > 0))
                        {
                            vanilIndeces.Remove(selectedPlayer);
                        }

                        continue;
                    }
                    if (vanilIndeces.Keys.Intersect(allyPlayers).Count() > 0)
                    {
                        var options = vanilIndeces.Keys.Intersect(allyPlayers).ToArray();
                        selectedPlayer = options[UnityEngine.Random.Range(0, options.Count())];

                        var randomIndex = vanilIndeces[selectedPlayer][UnityEngine.Random.Range(0, vanilIndeces[selectedPlayer].Count())];
                        vanilIndeces[selectedPlayer].Remove(randomIndex);

                        var cord = selectedPlayer.data.currentCards[randomIndex];

                        cardsToAdd.Add(cord);
                        removedCards[selectedPlayer].Add(randomIndex);

                        if (!(vanilIndeces[selectedPlayer].Count > 0))
                        {
                            vanilIndeces.Remove(selectedPlayer);
                        }

                        continue;
                    }
                    break;
                }
                foreach (var kvp in removedCards)
                {
                    var removed = ModdingUtils.Utils.Cards.instance.RemoveCardsFromPlayer(kvp.Key, kvp.Value.ToArray());
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(kvp.Key, removed);
                }
                ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cardsToAdd.ToArray(), false, null, null, null, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, cardsToAdd.ToArray());
            });
        }
    }
}
