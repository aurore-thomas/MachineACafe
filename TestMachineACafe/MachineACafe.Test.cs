using Hardware;
using MachineACafé.Test.Utilities;
using Xunit;
using Assert = Xunit.Assert;

namespace MachineACafé.Test;

//TODO : Mocks automatisés.

public class SoftwareMachineTest
{
    [Fact]
    public void AucuneAction()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineSpy();
        var brewer = new BrewerSpy();

        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachine)
            .AyantUnBrewer(brewer)
            .Build();

        // ALORS aucune invocation du Brewer ou de la ChangeMachine n'est effectuée
        Assert.True(changeMachine.Untouched);
        Assert.True(brewer.Untouched);
    }

    [Fact]
    public void CasNominal()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy(new BrewerStub());
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // QUAND on insère une somme supérieure ou égale au prix d'un café
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS MakeACoffee est appelé une fois sur le hardware
        Assert.Equal(1, brewer.MakeACoffeeInvocations);

        // ET CollectStoredMoney est appelé une fois sur le hardware
        Assert.Equal(1, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    [Fact]
    public void CasBrewerDéfaillant()
    {
        // ETANT DONNE une machine à café ayant un brewer défaillant
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        _ = new SoftwareMachineBuilder()
            .AyantUnBrewer(new BrewerDummy())
            .AyantUneChangeMachine(changeMachineSpy)
            .Build();

        // QUAND on insère une somme supérieure ou égale au prix d'un café
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS FlushStoredMoney est appelé une fois
        Assert.Equal(1, changeMachineSpy.FlushStoredMoneyInvocations);

        // ET CollectStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.CollectStoredMoneyInvocations);
    }

    [Fact]
    public void PasAssezArgent()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy();
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // QUAND on insère moins que le prix d'un café
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS MakeACoffee n'est pas appelé
        Assert.Equal(0, brewer.MakeACoffeeInvocations);

        // ET CollectStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney est appelé une fois
        Assert.Equal(1, changeMachineSpy.FlushStoredMoneyInvocations);
    }
}