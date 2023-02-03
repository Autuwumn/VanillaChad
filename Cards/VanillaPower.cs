using System.Reflection;
using UnboundLib.Cards;
using UnityEngine;
using RarityLib.Utils;
using ChadVanilla.IHook;
using UnboundLib;
using VanillaChad.MonoBehaviors;
using ModsPlus;
using System.Linq;

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
            var mono = player.gameObject.GetOrAddComponent<VanillaPower_Mono>();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
        }
        protected override GameObject GetCardArt()
        {
            return ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Gladious");
        }
        protected override string GetDescription()
        {
            return "Gain compounding stats for every vanilla card owned";
        }
        protected override CardInfoStat[] GetStats()
        {
            return new [] {
                new CardInfoStat
                {
                    amount = "+5%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "basic stats per"
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

namespace VanillaChad.MonoBehaviors
{
    [DisallowMultipleComponent]
    public class VanillaPower_Mono : PlayerHook, IPointEndHookHandler, IPointStartHookHandler
    {
        private void OnStart()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
        }

        public void OnPointStart()
        {
            double vanCards = 0.0;
            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                foreach (var vc in vanillaCards)
                {
                    if (player.data.currentCards[i].cardName == vc.cardName)
                    {
                        vanCards++;
                    }
                }
            }
            float multiplier = (float)System.Math.Pow(1.05,vanCards);
            gun.damage*=multiplier;
            gun.attackSpeed/=multiplier;
            gun.reloadTime/=multiplier;
        }
        public void OnPointEnd()
        {
            double vanCards = 0.0;
            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
                foreach (var vc in vanillaCards)
                {
                    if (player.data.currentCards[i].cardName == vc.cardName)
                    {
                        vanCards++;
                    }
                }
            }
            var multiplier = (float)System.Math.Pow(1.05,vanCards);   
            gun.damage/=multiplier;
            gun.attackSpeed*=multiplier;
            gun.reloadTime*=multiplier;
        }
    }
}
