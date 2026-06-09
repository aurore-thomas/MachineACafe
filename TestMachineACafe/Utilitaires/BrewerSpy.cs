using Hardware;

namespace MachineACafe.Test.Utilitaires
{
    internal class BrewerSpy : IBrewer
    {
        private readonly IBrewer _behavior;

        public ushort MakeACoffeInvocations { get; private set;  }

        public BrewerSpy() : this(new BrewerStub()) { }

        public BrewerSpy(IBrewer behavior)
        {
            _behavior = behavior;
        }

        public bool MakeACoffee()
        {
            MakeACoffeInvocations++;
            return _behavior.MakeACoffee();
        }

        public bool PourChocolate()
        {
            return _behavior.PourChocolate();
        }

        public bool PourMilk()
        {
            return (_behavior.PourMilk());
        }

        public bool PourSugar()
        {
            return (_behavior.PourSugar());
        }

        public bool PourWater()
        {
            return (_behavior.PourWater());
        }

        public bool TryPullWater()
        {
            return (_behavior.TryPullWater());
        }
    }
}
