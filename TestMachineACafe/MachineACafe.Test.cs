using Hardware;
using MachineACafé.Test.Utilities;
using Xunit;
using Assert = Xunit.Assert;
using LogicielMachineACafé = MachineACafé.Test.Utilities.SoftwareMachineBuilder;

namespace MachineACafé.Test;

public class SoftwareMachineTest
{
    [Fact]
    public void AucuneAction()
    {
        // ETANT DONNE une machine à café
        var(_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default; 

        // QUAND aucune action n'est réalisée

        // ALORS aucune invocation du Brewer ou de la ChangeMachine n'est effectuée
        changeMachineSpy.AucuneAction();
    }

    [Theory]
    [InlineData(CoinCode.FiftyCents)]
    [InlineData(CoinCode.OneEuro)]
    [InlineData(CoinCode.TwoEuros)]
    public void CasNominal(CoinCode coin)
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // QUAND on insère une somme supérieure ou égale au prix d'un café
        changeMachineFake.SimulerInsertionPièce(coin);

        // ALORS 1 café est servi
        brewerSpy.CafesServis(1);

        // ET l'argent est encaissé et le surplus d'argent est restitué
        changeMachineSpy.ArgentEncaisséEtSurplusRendu(1);
    }

    [Fact]
    public void CasBrewerDéfaillant()
    {
        // ETANT DONNE une machine à café ayant un brewer défaillant
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Defaillant;

        // QUAND on insère une somme supérieure ou égale au prix d'un café
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS l'argent est restitué
        changeMachineSpy.ArgentRestitué();
    }

    [Theory]
    [InlineData(CoinCode.FiveCents)]
    [InlineData(CoinCode.TenCents)]
    [InlineData(CoinCode.TwentyCents)]
    public void PasAssezArgent(CoinCode coin)
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // QUAND on insère 1 pièce qui vaut moins que le prix d'un café
        changeMachineFake.SimulerInsertionPièce(coin);

        // ALORS aucun café n'est servi
        brewerSpy.CafesServis(0);

        // ET l'argent inséré n'est pas collecté ni rendu (le monnayeur attend d'autres pièces)
        changeMachineSpy.ArgentEnAttente();
    }

    [Fact]
    public void Cas1Café2Pièces()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // Quand on insère 2 pièces de 20 centimes
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS MakeACoffee est appelé 1 fois sur le hardware
        Assert.Equal(1, brewerSpy.MakeACoffeeInvocations);

        // ET CollectStoredMoney est appelé 1 fois sur le hardware
        Assert.Equal(1, changeMachineSpy.CollectStoredMoneyInvocations);

        // ET FlushStoredMoney n'est pas appelé
        Assert.Equal(0, changeMachineSpy.FlushStoredMoneyInvocations);
    }

    [Fact]
    public void Cas1Café4Pièces()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // Quand on insère 4 pièces de 10 centimes
        changeMachineFake.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TenCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TenCents);

        // ALORS un café est servi
        brewerSpy.CafesServis(1);

        // ET l'argent est encaissé
        changeMachineSpy.ArgentEncaisséSansRendu(1);
    }

    [Fact]
    public void Cas2CafésPlusieursPièces()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // QUAND on insère 4 pièces de 20 centimes
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);

        // ALORS 2 cafés sont servis
        brewerSpy.CafesServis(2);

        // ET l'argent est encaissé 2 fois
        changeMachineSpy.ArgentEncaisséSansRendu(2);   
    }

    [Fact]
    public void Cas2CafésAvec50Cts()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // Quand on insère 2 pièces de 50 centimes
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiftyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS 2 cafés sont servis
        brewerSpy.CafesServis(2);

        // ET l'argent est encaissé 2 fois
        changeMachineSpy.ArgentEncaisséEtSurplusRendu(2);
    }

    [Fact]
    public void CasPasAssezArgentAvec5Pieces()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // Quand on insère 4 pièces de 5 centimes
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiveCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiveCents);

        // ALORS aucun café n'est servi
        brewerSpy.CafesServis(0);

        // ET l'argent est restitué
        changeMachineSpy.ArgentRestitué();
    }

    [Fact]
    public void CasDeuxPiècesInsuffisantes()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // QUAND on insère deux pièces insuffisantes pour le prix d'un café (20 et 10 centimes)
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.TenCents);

        // ALORS aucun café n'est servi
        brewerSpy.CafesServis(0);

        // ET l'argent est en attente (la machine attend d'autres pièces)
        changeMachineSpy.ArgentEnAttente();
    }

    [Fact]
    public void CasDeuxPiècesMonnaieEnTrop()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.Default;

        // QUAND on insère une pièce de 20 centimes et une pièce de 50 centimes
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS un café est servi
        brewerSpy.CafesServis(1);

        // ET l'argent est encaissé
        changeMachineSpy.ArgentEncaisséEtSurplusRendu(1);
    }

    [Theory]
    [InlineData(CoinCode.FiftyCents)]
    [InlineData(CoinCode.OneEuro)]
    [InlineData(CoinCode.TwoEuros)]
    public void CasMonnayeurVide(CoinCode coin)
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy, coinInMachine) = LogicielMachineACafé.NoCoinInMachine;

        // QUAND on insère 1 pièce suffisante pour le prix d'un café et le monnayeur est vide (pas de monnaie pour rendre la différence)
        changeMachineFake.SimulerInsertionPièce(coin);

        // ALORS 1 café est servi
        brewerSpy.CafesServis(1);

        // ET le surplus d'argent n'est pas rendu (la machine ne peut pas rendre la monnaie)
        changeMachineSpy.ArgentEncaisséSansRendu(1);
    }
}