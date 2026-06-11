using Hardware;

namespace MachineACafé.Test.Utilities;

internal class BrewerStub : IBrewer
{
    public bool MakeACoffee()
    {
        return true;
    }

    public bool TryPullWater()
    {
        return true;
    }

    public bool PourMilk()
    {
        return true;
    }

    public bool PourWater()
    {
        return true;
    }

    public bool PourSugar()
    {
        return true;
    }

    public bool PourChocolate()
    {
        return true;
    }
}