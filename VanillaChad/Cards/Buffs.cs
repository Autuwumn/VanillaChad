using RarityLib.Utils;
using ModsPlus;
using ClassesManagerReborn.Util;
using UnboundLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnityEngine;

namespace ChadVanilla.Cards
{
    public class Buff : SimpleCard
    {
        
        public static int chadCards = 0;
        internal static CardInfo card = null;
        
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Rejected Power",
            Description = "You have rejected your path",
            ModName     = ChadVanilla.ModInitials,
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Ripped"),
            Rarity      = RarityUtils.GetRarity("Divine"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
            CustomCardCategories.instance.MakeCardsExclusive(Buff.card, Chadious.card);
            CustomCardCategories.instance.MakeCardsExclusive(Buff.card, Rejectio.card);
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            card.SetAbbreviation(chadCards+"x");
            float mult = (float)System.Math.Pow(1.1,(double)chadCards);
            float halfMult = 0.5f+mult/2f;
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
            characterStats.movementSpeed*=halfMult;
            characterStats.jump*=halfMult;
            var baseAmmo = gunAmmo.maxAmmo;
            gunAmmo.maxAmmo+=(int)mult*baseAmmo;
            var baseReflects = gun.reflects;
            gun.reflects+=(int)mult*baseReflects;
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            float mult = (float)System.Math.Pow(1.1,(double)chadCards);
            float halfMult = 0.5f+mult/2f;
            gun.damage*=mult;
            gun.attackSpeed/=mult;
            gunAmmo.reloadTimeMultiplier/=mult;
            player.data.maxHealth*=mult;
            characterStats.movementSpeed*=halfMult;
            characterStats.jump*=halfMult;
            var baseAmmo = gunAmmo.maxAmmo;
            gunAmmo.maxAmmo+=(int)mult*baseAmmo;
            var baseReflects = gun.reflects;
            gun.reflects+=(int)mult*baseReflects;
        }
    }
}
