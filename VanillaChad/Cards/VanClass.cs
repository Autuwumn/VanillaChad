using ClassesManagerReborn;
using System.Collections;

namespace ChadVanilla.Cards
{
    class VanClass : ClassHandler
    {
        internal static string name = "Vanilla Chad";

        public override IEnumerator Init()
        {
            while (!(Chadious.card && VanPower.card && VanEnhan.card && VanThief.card && VanGains.card)) yield return null;
            ClassesRegistry.Register(Chadious.card, CardType.Entry);
            ClassesRegistry.Register(VanPower.card, CardType.Card, Chadious.card, 5);
            ClassesRegistry.Register(VanEnhan.card, CardType.Card, Chadious.card, 5);
            ClassesRegistry.Register(VanThief.card, CardType.Card, Chadious.card);
            ClassesRegistry.Register(VanGains.card, CardType.Card, Chadious.card);
            ClassesRegistry.Register(Rejectio.card, CardType.Card, Chadious.card);
        }
    }
}