using ClassesManagerReborn;
using System.Collections;

namespace ChadVanilla.Cards
{
    class VanClass : ClassHandler
    {
        internal static string name = "Chad Vanilla";

        public override IEnumerator Init()
        {
            while (!(Chadious.card && VanPower.card)) yield return null;
            ClassesRegistry.Register(Chadious.card, CardType.Entry);
            ClassesRegistry.Register(VanPower.card, CardType.Card, Chadious.card, 5);
        }
    }
}