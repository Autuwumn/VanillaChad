using System.Reflection;
using UnboundLib.Cards;
using UnityEngine;
using RarityLib.Utils;
using ChadVanilla.IHook;
using UnboundLib;
using VanillaChad.MonoBehaviors;
using ModsPlus;
using ClassesManagerReborn.Util;

namespace ChadVanilla.Cards
{
    class VanEnhance : CustomCard
    {
        //when extening this class, you only need to override the methods you need to change
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<VanEnhance_Mono>();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<VanEnhance_Mono>();
        }
        protected override GameObject GetCardArt()
        {
            return ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Enhancer");
        }
        protected override string GetDescription()
        {
            return "Enhance your vanilla cards";
        }
        protected override CardInfoStat[] GetStats()
        {
            return new [] {
                new CardInfoStat
                {
                    amount = "+20%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Positive Vanilla Card Stats"
                },
                new CardInfoStat
                {
                    amount = "-10%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Negative Vanilla Card Stats"
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
            return "Vanilla Enhancer" ;
        }
        public override string GetModName()
        {
            return "CHAD";
        }
    }
}

namespace VanillaChad.MonoBehaviors
{   
    [DisallowMultipleComponent]
    public class VanEnhance_Mono : PlayerHook, IPointEndHookHandler, IPointStartHookHandler
    {
        internal static int bounces = 0;
        public void OnPointStart()
        {
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            double boost = 0.0;
            for (int i = 0; i < player.data.currentCards.Count; i++) if (player.data.currentCards[i].cardName.ToLower() == "Vanilla Enhancer".ToLower()) boost++;
            float posMult = (float)System.Math.Pow(1.2,boost);
            float negMult = (float)System.Math.Pow(1.1,boost);
            StatChanges stoofs = new StatChanges{};
            for (int i = 0; i < player.data.currentCards.Count; i++)
            {
                /**foreach (var vc in vanillaCards)
                {
                    if(player.data.currentCards[i].cardName.ToLower() == vc.cardName.ToLower()) {
                        var cardStats = player.data.currentCards[i];
                    }
                }**/
                switch(player.data.currentCards[i].cardName.ToLower())
                {
                    case "abyssal countdown":
                        // N/A
                    break;
                    case "barrage":
                        stoofs.Damage*=1.0f+(0.7f*negMult-0.7f);
                        stoofs.MaxAmmo+=(int)boost;
                        stoofs.Bullets+=(int)(boost/2.0f);
                    break;
                    case "big bullet":
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "bombs away":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "bouncy":
                        stoofs.Damage*=1.0f+(0.25f*posMult-0.25f);
                        if (boost > 2) bounces++;
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "brawler":
                        // N/A
                    break;
                    case "buckshot":
                        stoofs.Damage*=1.0f+(0.6f*negMult-0.6f);
                        stoofs.MaxAmmo+=(int)boost;
                        stoofs.Bullets+=(int)(boost/2.0f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "burst":
                        stoofs.Damage*=1.0f+(0.6f*negMult-0.6f);
                        if (boost > 2) stoofs.Bullets+=1;
                        stoofs.MaxAmmo+=(int)(boost/2.0f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "careful planning":
                        stoofs.Damage*=1.0f+(1.0f*posMult-1.0f);
                        stoofs.AttackSpeed/=1.0f+(1.0f*negMult-1.0f);
                        // gunAmmo.reloadTimeAdd-=0.05f*(float)boost;
                    break;
                    case "chase":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "chilling presence":
                        stoofs.MaxHealth*=1.0f+(0.25f*posMult-0.25f);
                    break;
                    case "cold bullets":
                        // gun.slow*=1.0f+(0.7f*posMult-0.7f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "combine":
                        stoofs.Damage*=1.0f+(1.0f*posMult-1.0f);
                        if (boost > 2) stoofs.MaxAmmo++;
                        // gunAmmo.reloadTimeAdd-=0.05f*(float)boost;
                    break;
                    case "dazzle":
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "decay":
                        stoofs.MaxHealth*=1.0f+(0.5f*posMult-0.5f);
                    break;
                    case "defender":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                        // player.data.block.cdMultiplier/=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "demonic pact":
                        stoofs.Bullets+=(int)(boost*2.0f);
                        stoofs.MaxAmmo+=(int)(boost*2.0f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "drill ammo":
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "echo":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "emp":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "empower":
                        // N/A
                    break;
                    case "explosive bullet":
                        stoofs.AttackSpeed/=1.0f+(1.0f*negMult-1.0f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "fastball":
                        stoofs.BulletSpeed*=1.0f+(1.5f*posMult-1.5f);
                        stoofs.AttackSpeed/=1.0f+(0.5f*negMult-0.5f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "fast forward":
                        stoofs.BulletSpeed*=1.0f+(1.0f*posMult-1.0f);
                        // gunAmmo.reloadTimeMultiplier+=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "frost slam":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "glass cannon":
                        stoofs.Damage*=1.0f+(1.0f*posMult-1.0f);
                        stoofs.MaxHealth*=1.0f+(1.0f*negMult-1.0f);
                    break;
                    case "grow":
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "healing field":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "homing":
                        stoofs.Damage*=1.0f+(0.25f*negMult-0.25f);
                        stoofs.AttackSpeed/=1.0f+(0.5f*negMult-0.5f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "huge":
                        stoofs.MaxHealth*=1.0f+(0.8f*posMult-0.8f);
                    break;
                    case "implode":
                        stoofs.MaxHealth*=1.0f+(0.5f*posMult-0.5f);
                    break;
                    case "leech":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "lifestealer":
                        stoofs.MaxHealth*=1.0f+(0.25f*posMult-0.25f);
                    break;
                    case "mayhem":
                        stoofs.Damage*=1.0f+(0.15f*negMult-0.15f);
                        bounces+=(int)boost;
                        // gunAmmo.reloadTimeAdd-=0.05f*(float)boost;
                    break;
                    case "overpower":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "parasite":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                        stoofs.Damage*=1.0f+(0.3f*posMult-0.3f);
                        // gunAmmo.reloadTimeAdd-=0.025f*(float)boost;
                    break;
                    case "phoenix":
                        stoofs.MaxHealth*=1.0f+(0.35f*negMult-0.35f);
                    break;
                    case "poison":
                        stoofs.Damage*=1.0f+(0.7f*posMult-0.7f);
                        if (boost > 3) stoofs.MaxAmmo++;
                    break;
                    case "pristine perseverance":
                        // N/A
                    break;
                    case "quick reload":
                        // gunAmmo.reloadTimeMultiplier*=1.0f+(0.7f*posMult-0.7f);
                    break;
                    case "quick shot":
                        stoofs.BulletSpeed*=1.0f+(1.5f*posMult-1.5f);
                    break;
                    case "radar shot":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "radiance":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "refresh":
                        // N/A
                    break;
                    case "remote":
                        stoofs.BulletSpeed*=1.0f+(0.4f*posMult-0.4f);
                    break;
                    case "riccochet":
                        stoofs.AttackSpeed/=1.0f+(0.25f*posMult-0.25f);
                        if (boost > 2) bounces++;
                    break;
                    case "saw":
                        stoofs.MaxHealth*=1.0f+(0.3f*posMult-0.3f);
                    break;
                    case "scavenger":
                        // N/A
                    break;
                    case "shield charge":
                        // N/A
                    break;
                    case "shields up":
                        // N/A
                    break;
                    case "shockwave":
                        stoofs.MaxHealth*=1.0f+(0.5f*posMult-0.5f);
                    break;
                    case "silence":
                        stoofs.MaxHealth*=1.0f+(0.25f*posMult-0.25f);
                    break;
                    case "sneaky":
                        // N/A
                    break;
                    case "spray":
                        stoofs.AttackSpeed/=1.0f+(9.0f*posMult-9.0f);
                        stoofs.MaxAmmo+=(int)(boost*2);
                        stoofs.Damage*=1.0f+(0.75f*negMult-0.75f);
                    break;
                    case "static field":
                        // N/A
                    break;
                    case "steady shot":
                        stoofs.MaxHealth*=1.0f+(0.4f*posMult-0.4f);
                        stoofs.BulletSpeed*=1.0f+(1.0f*posMult-1.0f);
                    break;
                    case "supernova":
                        stoofs.MaxHealth*=1.0f+(0.5f*posMult-0.5f);
                    break;
                    case "tactical reload":
                        // N/A
                    break;
                    case "tank":
                        stoofs.MaxHealth*=1.0f+(1.0f*posMult-1.0f);
                        stoofs.AttackSpeed/=1.0f+(0.25f*posMult-0.25f);
                    break;
                    case "target bounce":
                        if (boost > 3) bounces++;
                        stoofs.Damage*=1.0f+(0.2f*negMult-0.2f);
                    break;
                    case "taste of blood":
                        // N/A
                    break;
                    case "teleport":
                        // N/A
                    break;
                    case "thruster":
                        // N/A
                    break;
                    case "timed detonation":
                        stoofs.Damage*=1.0f+(0.15f*negMult-0.15f);
                    break;
                    case "toxic cloud":
                        stoofs.Damage*=1.0f+(0.25f*negMult-0.25f);
                        stoofs.AttackSpeed/=1.0f+(0.2f*negMult-0.2f);
                    break;
                    case "trickster":
                        stoofs.Damage*=1.0f+(0.2f*negMult-0.2f);
                        if(boost > 2) bounces++;
                    break;
                    case "windup":
                        stoofs.BulletSpeed*=1.0f+(1.0f*posMult-1.0f);
                        stoofs.Damage*=1.0f+(0.6f*posMult-0.6f);
                        stoofs.AttackSpeed/=1.0f+(1.0f*posMult-1.0f);
                    break;
                }
            }
            StatManager.Apply(player, stoofs);
            gun.reflects+=bounces;
        }
        public void OnPointEnd()
        {
           gun.reflects-=bounces;
        }
    }
}
