using System.Reflection;
using UnboundLib.Cards;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;

namespace ChadVanilla.Cards
{
    class Blank : CustomCard
    {
        //when extening this class, you only need to override the methods you need to change
        internal static CardInfo card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override string GetDescription()
        {
            return "How did you find this?";
        }
        protected override CardInfoStat[] GetStats()
        {
            return new [] {
                new CardInfoStat
                {
                    amount = "Nothing",
                    positive = false,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Here"
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
            return "BlankedOut" ;
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
