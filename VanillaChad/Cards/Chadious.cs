using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using ClassesManagerReborn.Util;

namespace ChadVanilla.Cards
{
    class Chadious : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>();
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Vanilla Chad",
            Description = "Become a true vanilla chad",
            ModName     = "CHAD",
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_TrulyChad"),
            Rarity      = RarityUtils.GetRarity("Epic"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new []
            {
                new CardInfoStat
                {
                    amount = "Highly Increased",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Vanilla Card Chance"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (player.data.view.IsMine)
            {
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                foreach (var vc in vanillaCards)
                {
                    RarityUtils.AjustCardRarityModifier(vc, 0.0f, 9.0f);
                }
            }
        }
        
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (player.data.view.IsMine)
            {
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                foreach (var vc in vanillaCards)
                {
                    RarityUtils.AjustCardRarityModifier(vc, 0.0f, -9.0f);
                }
            }
        }
    }
}
