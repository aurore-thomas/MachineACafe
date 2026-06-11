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

        // ET FlushStoredMoney est appelé 1 fois (le monnayeur attend d'autres pièces)
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    [Fact]
    public void Cas1Café2Pièces()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy(new BrewerStub());
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // Quand on insère 4 pièces de 20 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS MakeACoffee est appelé 1 fois sur le hardware
        Assert.Equal(1, brewer.MakeACoffeeInvocations);

        // ET CollectStoredMoney est appelé 1 fois sur le hardware
        Assert.Equal(1, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    [Fact]
    public void Cas1Café4Pièces()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy(new BrewerStub());
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // Quand on insère 4 pièces de 20 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);

        // ALORS MakeACoffee est appelé 1 fois sur le hardware
        Assert.Equal(1, brewer.MakeACoffeeInvocations);

        // ET CollectStoredMoney est appelé 1 fois sur le hardware
        Assert.Equal(1, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    [Fact]
    public void Cas2CafésPlusieursPièces()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy(new BrewerStub());
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // Quand on insère 4 pièces de 20 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS MakeACoffee est appelé 2 fois sur le hardware
        Assert.Equal(2, brewer.MakeACoffeeInvocations);

        // ET CollectStoredMoney est appelé 2 fois sur le hardware
        Assert.Equal(2, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    [Fact]
    public void CasPasAssezArgentAvec5Pieces()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy(new BrewerStub());
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // Quand on insère 4 pièces de 20 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);

        // ALORS MakeACoffee est appelé 0 fois sur le hardware
        Assert.Equal(0, brewer.MakeACoffeeInvocations);

        // ET CollectStoredMoney est appelé 0 fois sur le hardware
        Assert.Equal(0, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney est 1 fois appelé
        Assert.Equal(1, changeMachineSpy.FlushStoredMoneyInvocations);
    }
}