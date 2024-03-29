﻿using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using ClassesManagerReborn.Util;
using System.Collections.Generic;

namespace ChadVanilla.Cards
{
    public class VanColor : SimpleCard
    {
        internal static CardInfo card = null;
        internal bool usedUp = false;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Vanilla Colors",
            Description = "Gain all vanilla cards of a random color",
            ModName = ChadVanilla.ModInitials,
            Art = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Summoner"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.MagicPink
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (usedUp) return;
            ChadVanilla.instance.ExecuteAfterFrames(20, () =>
            {
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                int cardsGaining = UnityEngine.Random.Range(1, 5);
                List<CardInfo> cardsToAdd = new List<CardInfo>();
                while (cardsToAdd.Count < cardsGaining)
                {
                    CardInfo card = vanillaCards[UnityEngine.Random.Range(0, vanillaCards.Length)];
                    cardsToAdd.Add(card);
                }
                ModdingUtils.Utils.Cards.instance.AddCardsToPlayer(player, cardsToAdd.ToArray(), false, null, null, null, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, cardsToAdd.ToArray());
                usedUp = true;
            });
        }
    }
}
