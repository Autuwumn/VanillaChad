using BepInEx;
using HarmonyLib;
using ChadVanilla.Cards;
using UnboundLib.Cards;
using Jotunn.Utils;
using UnityEngine;
using ModsPlus;
using ChadVanilla.IHook;

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
        public const string Version = "1.5.2";
        public const string ModInitials = "CHAD";

        internal static ChadVanilla instance;

        internal static AssetBundle ArtAssets;

        void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;
            
            gameObject.AddComponent<InterfaceGameModeHooksManager>();
            
            ChadVanilla.ArtAssets = AssetUtils.LoadAssetBundleFromResources("chadvan", typeof(ChadVanilla).Assembly);

            if (ChadVanilla.ArtAssets == null)
            {
                UnityEngine.Debug.Log("Chad Vanilla art asset bundle either doesn't exist or failed to load.");
            }

            CustomCard.BuildCard<Chadious>((card) => {Chadious.card = card; card.SetAbbreviation("VC");});
            CustomCard.BuildCard<VanPower>((card) => {VanPower.card = card; card.SetAbbreviation("VP");});
            CustomCard.BuildCard<VanEnhance>((card) => {VanEnhance.card = card; card.SetAbbreviation("VE");});
            CustomCard.BuildCard<VanThief>((card) => {VanThief.card = card; card.SetAbbreviation("VT");});

        }
        public static bool Debug = false;
    }
}