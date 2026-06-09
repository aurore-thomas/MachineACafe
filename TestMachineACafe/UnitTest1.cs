using Hardware;
using MachineACafe.Test.Utilitaires;
using SoftwareMachine;
using Xunit;
using Assert = Xunit.Assert;

namespace TestMachineACafe;

public class Tests
{
    [Fact(DisplayName = "Quand la bonne somme est insérée, un café est servi")]
    public void CasNominal()
    {
        // Etant donné une machine à café
        var brewer = new BrewerSpy(new BrewerStub());
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var machineACafe = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachine)
            .AyantUnBrewer(brewer)
            .Build();

        var nbCafesInitiaux = brewer.MakeACoffeInvocations;

        // Quand le hardware signale une somme suffisante pour le prix d'un café
        machineACafe.InsererPiece(SoftwareMachineClass.PrixDuCafeEnCentimes);

        // ALORS MakeACoffee est appelé 1 fois 
        Assert.Equal(nbCafesInitiaux + 1, brewer.MakeACoffeInvocations);

        // ET CollectStoredMoney est appelé 1 fois 
        Assert.Equal(SoftwareMachineClass.PrixDuCafeEnCentimes, changeMachine.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé 
        Assert.Equal(0, changeMachine.FlushStoredMoneyInvocations);

    }

    [Fact(DisplayName = "Quand la bonne somme est insérée 2 fois, deux cafés sont servis")]
    public void Cas2Cafes()
    {
        // Etant donné une machine à café
        var brewer = new BrewerSpy(new BrewerStub());
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var machineACafe = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachine)
            .AyantUnBrewer(brewer)
            .Build();
        var nbCafesInitiaux = machineACafe.NombreCafeServi;

        // Quand le hardware signale une somme suffisante pour le prix de deux cafés
        machineACafe.InsererPiece(SoftwareMachineClass.PrixDuCafeEnCentimes);
        machineACafe.InsererPiece(SoftwareMachineClass.PrixDuCafeEnCentimes);

        // ALORS MakeACoffee est appelé 2 fois 
        Assert.Equal(nbCafesInitiaux + 2, brewer.MakeACoffeInvocations);

        // ET CollectStoredMoney est appelé 1 fois 
        Assert.Equal(SoftwareMachineClass.PrixDuCafeEnCentimes * 2, changeMachine.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé 
        Assert.Equal(0, changeMachine.FlushStoredMoneyInvocations);
    }

    [Fact(DisplayName = "Quand trop d'argent est inséré, un café coule et l'argent est encaissé (le surplus n'est pas rendu)")]
    public void CasTropArgent()
    {
        // Etant donné une machine à café
        var brewer = new BrewerSpy(new BrewerStub());
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var machineACafe = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachine)
            .AyantUnBrewer(brewer)
            .Build();
        var nbCafesInitiaux = machineACafe.NombreCafeServi;

        // Quand le hardware signale une somme suffisante pour le prix de deux cafés
        machineACafe.InsererPiece(SoftwareMachineClass.PrixDuCafeEnCentimes + 2);

        // ALORS MakeACoffee est appelé 1 fois sur le hardware 
        Assert.Equal(1, brewer.MakeACoffeInvocations);

        // ET CollectStoredMoney est appelé 1 fois sur le hardware
        Assert.Equal(SoftwareMachineClass.PrixDuCafeEnCentimes, changeMachine.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé 
        Assert.Equal(0, changeMachine.FlushStoredMoneyInvocations);
    }

    [Fact(DisplayName = "Quand pas assez d'argent est inséré, le café ne coule pas et l'argent est rendu")]
    public void CasPasAssezArgent()
    {
        // Etant donné une machine à café
        var brewer = new BrewerSpy(new BrewerStub());
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var machineACafe = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachine)
            .AyantUnBrewer(brewer)
            .Build();

        // Quand le hardware signale une somme suffisante pour le prix d'un café
        machineACafe.InsererPiece(SoftwareMachineClass.PrixDuCafeEnCentimes - 1);

        // ALORS MakeACoffee n'est pas appelé 
        Assert.Equal(0, brewer.MakeACoffeInvocations);

        // ET FlushStore est appelé 1 fois 
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);

        // ET CollectStoredMoney n'est pas appelé 
        Assert.Equal(0, changeMachine.CollectStoredMoneyInvocations);
    }

    [Fact(DisplayName = "Quand aucune somme n'est insérée, aucun café n'est servi.")]
    public void CasRien()
    {
        // ETANT DONNE une machine a café
        var brewer = new BrewerSpy(new BrewerStub());
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());
        var machineACafe = new SoftwareMachineBuilder()
            .AyantUneChangeMachine(changeMachine)
            .AyantUnBrewer(brewer)
            .Build();

        // Quand rien

        // ALORS aucun café n'est servi
        Assert.Equal(0, brewer.MakeACoffeInvocations);
    }

    [Fact(DisplayName = "Quand la machine à café est défaillante, celle-ci rend l'argent inséré")]
    public void CasBrewerDefaillant()
    {
        // Etant donné une machine à café
        var changeMachine = new ChangeMachineSpy(new ChangeMachineStub());

        var machineACafe = new SoftwareMachineBuilder()
            .AyantUnBrewerDefaillant()
            .AyantUneChangeMachine(changeMachine)
            .Build();
        var nbCafesInitiaux = machineACafe.NombreCafeServi;

        // Quand on insère 40 centimes 
        machineACafe.InsererPiece(SoftwareMachineClass.PrixDuCafeEnCentimes);

        // ET FlushStore est appelé 1 fois 
        Assert.Equal(1, changeMachine.FlushStoredMoneyInvocations);

        // ET CollectStoredMoney n'est pas appelé 
        Assert.Equal(0, changeMachine.CollectStoredMoneyInvocations);
    }

}
