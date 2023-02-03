using ClassesManagerReborn;
using System.Collections;

namespace ChadVanilla.Cards
{
    class VanClass : ClassHandler
    {
        internal static string name = "Vanilla Chad";

        public override IEnumerator Init()
        {
            while (!(Chadious.card && VanPower.card && VanEnhance.card)) yield return null;
            ClassesRegistry.Register(Chadious.card, CardType.Entry);
            ClassesRegistry.Register(VanPower.card, CardType.Card, Chadious.card, 5);
            //ClassesRegistry.Register(VanEnhance.card, CardType.Card, Chadious.card, 5);
        }
    }
}