using BepInEx;
using HarmonyLib;
using ChadVanilla.Cards;
using UnboundLib.Cards;
using Jotunn.Utils;
using UnityEngine;
using ModsPlus;

namespace ChadVanilla
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willis.rounds.modsplus", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class ChadVanilla : BaseUnityPlugin
    {
        private const string ModId = "koala.vanilla.chad";
        private const string ModName = "Chad Vanilla";
        public const string Version = "1.5.7";
        public const string ModInitials = "CHAD";

        internal static ChadVanilla instance;
        internal static AssetBundle ArtAssets;

        void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;
            
            
            ChadVanilla.ArtAssets = AssetUtils.LoadAssetBundleFromResources("chadvan", typeof(ChadVanilla).Assembly);

            if (ChadVanilla.ArtAssets == null)
            {
                UnityEngine.Debug.Log("Chad Vanilla art asset bundle either doesn't exist or failed to load.");
            }

            CustomCard.BuildCard<Chadious>((card) => {Chadious.card = card; card.SetAbbreviation("Vc");});
            CustomCard.BuildCard<VanPower>((card) => {VanPower.card = card; card.SetAbbreviation("Vp");});
            CustomCard.BuildCard<VanEnhan>((card) => {VanEnhan.card = card; card.SetAbbreviation("Ve");});
            CustomCard.BuildCard<VanThief>((card) => {VanThief.card = card; card.SetAbbreviation("Vt");});
            CustomCard.BuildCard<VanGains>((card) => {VanGains.card = card; card.SetAbbreviation("Vs");});
            
            CustomCard.BuildCard<Rejectio>((card) => {Rejectio.card = card; card.SetAbbreviation("Re");});
            CustomCard.BuildCard<Buff>((card) => {Buff.card = card; ModdingUtils.Utils.Cards.instance.AddHiddenCard(card);});
        }
        public static bool Debug = false;
    }
}