using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace VanillaChad
{
    public class StutManager
    {
        public static StutChangeTracker Apply(Player player, StutChanges stuts)
        {
            var effect = player.gameObject.AddComponent<ReloadTemp>();
            return effect.Initialize(stuts);
        }

        public static void Remove(StutChangeTracker stutus)
        {
            if(!stutus.active) return;
            UnityEngine.Object.Destroy(stutus.effect);
        }
    }

    public class StutChangeTracker
    {
        public bool active;
        internal ReloadTemp effect;

        internal StutChangeTracker(ReloadTemp effect)
        {
            this.effect = effect;
        }
    }
    [Serializable]
    public class StutChanges
    {
        public int
            ReloadTimeAdd = 0,
            Bounces = 0;

        public float
            ReloadTimeMult = 1;
    }

     internal class ReloadTemp : ReversibleEffect
    {
        private StutChanges stutChanges;
        private StutChangeTracker stutus;

        public StutChangeTracker Initialize(StutChanges stuts)
        {
            this.stutChanges = stuts;
            this.stutus = new StutChangeTracker(this);
            return stutus;
        }

        public override void OnStart()
        {
            gunStatModifier.reflects_add = stutChanges.Bounces;

            gunAmmoStatModifier.reloadTimeAdd_add = stutChanges.ReloadTimeAdd;
            gunAmmoStatModifier.reloadTimeMultiplier_mult = stutChanges.ReloadTimeMult;

            stutus.active = true;
        }

        public override void OnOnDestroy()
        {
            stutus.active = false;
        }
    }
}