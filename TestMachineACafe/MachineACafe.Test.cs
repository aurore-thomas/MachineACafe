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
        var(_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Default; 

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
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Default;

        // QUAND on insère une somme supérieure ou égale au prix d'un café
        changeMachineFake.SimulerInsertionPièce(coin);

        // ALORS 1 café est servi
        brewerSpy.CafesServis(1);

        // ET l'argent est encaissé
        changeMachineSpy.ArgentEncaissé(1);
    }

    [Fact]
    public void CasBrewerDéfaillant()
    {
        // ETANT DONNE une machine à café ayant un brewer défaillant
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Defaillant;

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
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Default;

        // QUAND on insère 1 pièce qui vaut moins que le prix d'un café
        changeMachineFake.SimulerInsertionPièce(coin);

        // ALORS aucun café n'est servi
        brewerSpy.CafesServis(0);

        // ET l'argent inséré n'est pas collecté ni rendu (le monnayeur attend d'autres pièces)
        changeMachineSpy.ArgentEnAttente();
    }

    [Fact]
    public void CasPasAssezArgentAvec5Pieces()
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Default;

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
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Default;

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
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Default;

        // QUAND on insère une pièce de 20 centimes et une pièce de 50 centimes
        changeMachineFake.SimulerInsertionPièce(CoinCode.TwentyCents);
        changeMachineFake.SimulerInsertionPièce(CoinCode.FiftyCents);

        // ALORS un café est servi
        brewerSpy.CafesServis(1);

        // ET l'argent est encaissé
        changeMachineSpy.ArgentEncaissé(1);
    }
    
    [Theory]
    [MemberData(nameof(CafesEtEncaissementData))]
    public void CafesEtEncaissement(CoinCode[] coins, ushort cafesServis, ushort argentEncaissé)
    {
        // ETANT DONNE une machine à café
        var (_, brewer, changeMachine, changeMachineFake, changeMachineSpy, brewerSpy) = LogicielMachineACafé.Default;

        // Pour chaque pièce insérée 
        foreach (var coin in coins)
            changeMachineFake.SimulerInsertionPièce(coin);

        // ALORS Le café est servi n fois
        brewerSpy.CafesServis(cafesServis);
        
        // ET l'argent est encaissé n fois 
        changeMachineSpy.ArgentEncaissé(argentEncaissé);
    }

    public static IEnumerable<object[]> CafesEtEncaissementData =>
    [
        [new[] { CoinCode.TwentyCents, CoinCode.TwentyCents }, 1, 1],
        [new[] { CoinCode.FiftyCents, CoinCode.FiftyCents }, 2, 2],
        [new[] { CoinCode.TenCents, CoinCode.TenCents, CoinCode.TenCents, CoinCode.TenCents }, 1, 1],
        [new[] { CoinCode.TwentyCents, CoinCode.TwentyCents, CoinCode.TwentyCents, CoinCode.TwentyCents }, 2, 2],
    ];
}