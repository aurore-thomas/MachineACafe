using Hardware;

namespace MachineACafé.Test.Utilities;

internal class BrewerDummy : IBrewer
{
    public bool MakeACoffee()
    {
        throw new Exception("Défaillant");
    }

    public bool TryPullWater()
    {
        throw new Exception("Défaillant");
    }

    public bool PourMilk()
    {
        throw new Exception("Défaillant");
    }

    public bool PourWater()
    {
        throw new Exception("Défaillant");
    }

    public bool PourSugar()
    {
        throw new Exception("Défaillant");
    }

    public bool PourChocolate()
    {
        throw new Exception("Défaillant");
    }
}