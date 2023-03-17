using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using UnityEngine;

namespace ChadVanilla.Cards
{
    public class Buff : SimpleCard
    {
        
        public static int chadCards = 0;
        internal static CardInfo card = null;
        private int realAmount = -2;
        
        public override CardDetails Details => new CardDetails
        {
            Title       = "Rejected Power",
            Description = "You have rejected your path",
            ModName     = ChadVanilla.ModInitials,
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Ripped"),
            Rarity      = RarityUtils.GetRarity("Divine"),
            Theme       = CardThemeColor.CardThemeColorType.MagicPink
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if(realAmount == -2) realAmount = chadCards;
            card.SetAbbreviation(realAmount+"");
            float mult = (float)System.Math.Pow(1.15,(double)realAmount);
            cardInfo.cardStats = new []
            {
                new CardInfoStat
                {
                    amount = "+"+((mult-1)*100)+"%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "stats"
                }
            };
            gun.damage*=mult;
            gun.attackSpeed/=mult;
            gunAmmo.reloadTimeMultiplier/=mult;
            player.data.maxHealth*=mult;
            gunAmmo.maxAmmo+=realAmount*3;
            gun.reflects+=realAmount;
        }
    }
}
