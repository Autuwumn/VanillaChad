using System.Reflection;
using UnboundLib.Cards;
using UnityEngine;
using RarityLib.Utils;

namespace ChadVanilla.Cards
{
    class VanPower : CustomCard
    {
        //when extening this class, you only need to override the methods you need to change
        internal static CardInfo card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
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
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
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
        protected override GameObject GetCardArt()
        {
            return ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Gladious");
        }
        protected override string GetDescription()
        {
            return "Gain stats for every vanilla card";
        }
        protected override CardInfoStat[] GetStats()
        {
            return new [] {
                new CardInfoStat
                {
                    amount = "+1%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Basic Stats"
                },
                new CardInfoStat
                {
                    amount = null,
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "per vanila card"
                }
            };
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Epic");
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
        protected override string GetTitle()
        {
            return "Vanilla Power" ;
        }
        public override string GetModName()
        {
            return "CHAD";
        }
        /*
        public static string CardName<T>() where T : CardBase;
        {
            return T.Name.Replace('_', ' '); ;
        }*/
    }
}
