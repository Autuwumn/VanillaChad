using BepInEx;
using HarmonyLib;
using ChadVanilla.Cards;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib.Utils;
using RarityLib;
using RarityLib.Utils;
using Jotunn.Utils;
using UnityEngine;

namespace ChadVanilla
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class ChadVanilla : BaseUnityPlugin
    {
        private const string ModId = "koala.vanilla.chad";
        private const string ModName = "Chad Vanilla";
        public const string Version = "0.1.0";
        public const string ModInitials = "CHAD";

        internal static ChadVanilla instance;


        void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;


            CustomCard.BuildCard<Chadious>((card) => Chadious.card = card);

        }

        public static bool Debug = false;
    }
}