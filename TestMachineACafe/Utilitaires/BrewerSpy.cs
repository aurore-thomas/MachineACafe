using Hardware;

namespace MachineACafé.Test.Utilities;

internal class BrewerSpy : IBrewer
{
    private readonly IBrewer _behavior;

    public ushort MakeACoffeeInvocations { get; private set; }
    public bool Untouched => MakeACoffeeInvocations == 0;

    public BrewerSpy() : this(new BrewerStub()) { }

    public BrewerSpy(IBrewer behavior)
    {
        _behavior = behavior;
    }

    public bool MakeACoffee()
    {
        MakeACoffeeInvocations++;
        return _behavior.MakeACoffee();
    }

    public bool TryPullWater()
    {
        return _behavior.TryPullWater();
    }

    public bool PourMilk()
    {
        return _behavior.PourMilk();
    }

    public bool PourWater()
    {
        return _behavior.PourWater();
    }

    public bool PourSugar()
    {
        return _behavior.PourSugar();
    }

    public bool PourChocolate()
    {
        return _behavior.PourChocolate();
    }
}