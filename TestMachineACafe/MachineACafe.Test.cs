using Hardware;
using MachineACafé.Test.Utilities;
using Xunit;
using Assert = Xunit.Assert;

namespace MachineACafé.Test;

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

        // QUAND aucune action n'est réalisée

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

        // ALORS 1 café est servi
        brewer.CafesServis(1);

        // ET l'argent est encaissé
        changeMachineSpy.ArgentEncaissé(1);
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

        // ALORS l'argent est restitué
        changeMachineSpy.ArgentRestitué();
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

        // QUAND on insère 1 pièce qui vaut moins que le prix d'un café
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS aucun café n'est servi
        brewer.CafesServis(0);

        // ET l'argent inséré n'est pas collecté ni rendu (le monnayeur attend d'autres pièces)
        changeMachineSpy.ArgentEnAttente();
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

        // Quand on insère 2 pièces de 20 centimes
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

        // Quand on insère 4 pièces de 10 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);

        // ALORS un café est servi
        brewer.CafesServis(1);

        // ET l'argent est encaissé
        changeMachineSpy.ArgentEncaissé(1);
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

        // QUAND on insère 4 pièces de 20 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS 2 cafés sont servis
        brewer.CafesServis(2);

        // ET l'argent est encaissé 2 fois
        changeMachineSpy.ArgentEncaissé(2);   
    }

    [Fact]
    public void Cas2CafésAvec50Cts()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy(new BrewerStub());
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // Quand on insère 2 pièces de 50 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS 2 cafés sont servis
        brewer.CafesServis(2);

        // ET l'argent est encaissé 2 fois
        changeMachineSpy.ArgentEncaissé(2);
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

        // Quand on insère 4 pièces de 5 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiveCents);

        // ALORS aucun café n'est servi
        brewer.CafesServis(0);

        // ET l'argent est restitué
        changeMachineSpy.ArgentRestitué();
    }

    [Fact]
    public void CasDeuxPiècesInsuffisantes()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy();
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // QUAND on insère deux pièces insuffisantes pour le prix d'un café (20 et 10 centimes)
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.TenCents);

        // ALORS MakeACoffee n'est pas appelé
        Assert.Equal(0, brewer.MakeACoffeeInvocations);

        // ET CollectStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.CollectStoredMoneyInvocations);
        // la machine est en attente de plus de pieces
        Assert.Equal(2, changeMachineSpy.FlushStoredMoneyInvocations);
    }


    [Fact]
    public void CasDeuxPiècesMonnaieEnTrop()
    {
        // ETANT DONNE une machine à café
        var changeMachine = new ChangeMachineFake();
        var changeMachineSpy = new ChangeMachineSpy(changeMachine);

        var brewer = new BrewerSpy();
        _ = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachineSpy)
            .AyantUnBrewer(brewer)
            .Build();

        // QUAND on insère une pièce de 20 centimes et une pièce de 50 centimes
        changeMachine.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachine.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS un café est servi
        Assert.Equal(1, brewer.MakeACoffeeInvocations);
    }
}