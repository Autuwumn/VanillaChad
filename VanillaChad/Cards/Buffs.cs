using RarityLib.Utils;
using ModsPlus;
using ClassesManagerReborn.Util;
using UnboundLib;

namespace ChadVanilla.Cards
{
    public class Buff : SimpleCard
    {
        
        public static int chadCards = 0;
        internal static CardInfo card = null;

        float[] deltas;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Rejected Power",
            Description = "You have rejected your path",
            ModName     = ChadVanilla.ModInitials,
            Art         = null,
            Rarity      = RarityUtils.GetRarity("Epic"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            card.SetAbbreviation(chadCards+"x");
            float mult = (float)System.Math.Pow(1.2,(double)chadCards);
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
            // dmg, attackspeed, reloadtime, health, movespeed, jumpheight
            deltas = new[] {gun.damage*mult,gun.attackSpeed/mult,gunAmmo.reloadTimeMultiplier/mult,player.data.maxHealth*mult,characterStats.movementSpeed*mult,characterStats.jump*mult};
            /**gun.damage+=deltas[0];
            gun.attackSpeed-=deltas[1];
            gunAmmo.reloadTimeMultiplier-=deltas[2];
            player.data.maxHealth+=deltas[3];
            characterStats.movementSpeed+=deltas[4];
            characterStats.jump+=deltas[5];**/
            gun.damage*=mult;
            gun.attackSpeed/=mult;
            gunAmmo.reloadTimeMultiplier/=mult;
            player.data.maxHealth*=mult;
            characterStats.movementSpeed*=mult;
            characterStats.jump*=mult;
            gunAmmo.maxAmmo+=chadCards;
            gun.reflects+=chadCards;
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            float mult = (float)System.Math.Pow(1.2,(double)chadCards);
            gun.damage/=mult;
            gun.attackSpeed*=mult;
            gunAmmo.reloadTimeMultiplier*=mult;
            player.data.maxHealth/=mult;
            characterStats.movementSpeed/=mult;
            characterStats.jump/=mult;
            gunAmmo.maxAmmo-=chadCards;
            gun.reflects-=chadCards;
        }
    }
}
