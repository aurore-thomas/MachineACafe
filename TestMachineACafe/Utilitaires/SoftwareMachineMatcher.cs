using Xunit;

namespace MachineACafé.Test.Utilities;

internal static class SoftwareMachineMatcher
{
    public static void ArgentRestitué(this ChangeMachineSpy changeMachineSpy)
    {
        Assert.Equal(0, changeMachineSpy.CollectStoredMoneyInvocations);
        Assert.Equal(1, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    public static void ArgentEncaissé(this ChangeMachineSpy changeMachineSpy, ushort nbEncaissement)
    {
        Assert.Equal(nbEncaissement, changeMachineSpy.CollectStoredMoneyInvocations);
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    public static void ArgentEnAttente(this ChangeMachineSpy changeMachineSpy)
    {
        Assert.Equal(0, changeMachineSpy.CollectStoredMoneyInvocations);
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    public static void CafesServis(this BrewerSpy brewerSpy, ushort nbCafes)
    {
        Assert.Equal(nbCafes, brewerSpy.MakeACoffeeInvocations);
    }
}
