﻿using BepInEx;
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
        public const string Version = "1.4.1";
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

            CustomCard.BuildCard<Chadious>(c => c.SetAbbreviation("VC"));
            CustomCard.BuildCard<VanPower>(c => c.SetAbbreviation("VP"));

        }
        public static bool Debug = false;
    }
}