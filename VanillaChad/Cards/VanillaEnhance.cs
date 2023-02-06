using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using UnboundLib;
using VanillaChad.MonoBehaviors;
using ModsPlus;
using ClassesManagerReborn.Util;

namespace ChadVanilla.Cards
{
    class VanEnhance :  CustomEffectCard<VanEnhance_Mono>
    {
        //when extening this class, you only need to override the methods you need to change
        internal static CardInfo card = null;
        
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = VanClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title       = "Vanilla Enhance",
            Description = "Enhance your vanilla cards",
            ModName     = "CHAD",
            Art         = ChadVanilla.ArtAssets.LoadAsset<GameObject>("C_Enhancer"),
            Rarity      = RarityUtils.GetRarity("Epic"),
            Theme       = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new []
            {
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
            }
        };
    }
}

namespace VanillaChad.MonoBehaviors
{   
    [DisallowMultipleComponent]
    public class VanEnhance_Mono : CardEffect
    {
        internal static int bounces = 0;
        private void GiveBuffs()
        {
            bounces = 0;
            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            double boost = 0.0;
            for (int i = 0; i < player.data.currentCards.Count; i++) if (player.data.currentCards[i].cardName.ToLower() == "Vanilla Enhance".ToLower()) boost++;
            float posMult = (float)System.Math.Pow(1.2,boost);
            float negMult = (float)System.Math.Pow(1.1,boost);
            StatChanges stoofs = new StatChanges{      
                Bullets = 0,
                Jumps = 0,
                MaxAmmo = 0,

                AttackSpeed = 1.0f,
                PlayerGravity = 1.0f,
                MovementSpeed = 1.0f,
                ProjectileGravity = 1.0f,
                Damage = 1.0f,
                PlayerSize = 1.0f,
                MaxHealth = 1.0f,
                BulletSpread = 1.0f,
                BulletSpeed = 1.0f,
                JumpHeight = 1.0f
            };
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
                    case "abyssalcountdown":
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
                    case "bombsaway":
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
                    case "chillingpresence":
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
                    case "drillammo":
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
                    case "fast ball":
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
                    case "glasscannon":
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
                    case "poison bullets":
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
                    case "radarshot":
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
                    case "sneaky bullets":
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
                    case "targetbounce":
                        if (boost > 3) bounces++;
                        stoofs.Damage*=1.0f+(0.2f*negMult-0.2f);
                    break;
                    case "tasteofblood":
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
                    case "wind up":
                        stoofs.BulletSpeed*=1.0f+(1.0f*posMult-1.0f);
                        stoofs.Damage*=1.0f+(0.6f*posMult-0.6f);
                        stoofs.AttackSpeed/=1.0f+(1.0f*posMult-1.0f);
                    break;
                }
            }
            StatManager.Apply(player, stoofs);
            UnityEngine.Debug.Log(stoofs.Damage);
            UnityEngine.Debug.Log(boost);
        }
        
        public void OnPointStart() {
            GiveBuffs();
            gun.reflects+=bounces;
        }       
        public override void OnRevive() {
            GiveBuffs();
        }
        public void OnPointEnd()
        {
            gun.reflects-=bounces;
        }
    }
}
    