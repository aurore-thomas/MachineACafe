using Hardware;
using MachineACafe.Test.Utilitaires;
using Moq;

namespace MachineACafé.Test.Utilities;

internal class SoftwareMachineBuilder
{
    private IBrewer _brewer = BrewerDouble.FactoryStub().Object;
    private BrewerSpy? _brewerSpy;

    private ChangeMachineFake _changeMachineFake = new();
    private ChangeMachineSpy? _changeMachineSpy;
    private Dictionary<CoinCode, ushort> _coinInMachine { get; set; } = new();

    // Retourne aussi le fake et le spy pour les tests qui en ont besoin.
    public static (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine)
        Default => new SoftwareMachineBuilder().Build();

    public static (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine)
        Defaillant => new SoftwareMachineBuilder().BuildDefaillant();

    public static (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine)
        NoCoinInMachine => new SoftwareMachineBuilder().BuildNoCoin();

    // Cas où la machine a uniquement des pièces de 5 centimes (Utile pour tester l'adaptation)
    public static (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine)
        UniquementCinqCents => new SoftwareMachineBuilder().BuildSpecifique(CoinCode.FiveCents, 10);

    // Cas où la machine n'a qu'une seule pièce précise (Utile pour tester l'épuisement du stock)
    public static (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine)
        UneSeulePieceDeDixCents => new SoftwareMachineBuilder().BuildSpecifique(CoinCode.TenCents, 1);

    public (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine) Build()
    {
        _changeMachineSpy ??= new ChangeMachineSpy(_changeMachineFake);

        if (_brewerSpy is null)
        {
            _brewerSpy = new BrewerSpy(new BrewerStub());
            _brewer = _brewerSpy;
        }

        _coinInMachine = new Dictionary<CoinCode, ushort>
        {
            { CoinCode.FiveCents, 10 },
            { CoinCode.TenCents, 10 },
            { CoinCode.TwentyCents, 10 },
            { CoinCode.FiftyCents, 10 },
            { CoinCode.OneEuro, 10 },
            { CoinCode.TwoEuros, 10 }
        };

        var instance = new SoftwareMachineClass(_brewer, _changeMachineSpy, _coinInMachine);
        return (instance, _brewer, _changeMachineSpy, _changeMachineFake, _changeMachineSpy, _brewerSpy!, _coinInMachine);
    }

    public (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine) BuildDefaillant()
    {
        _changeMachineSpy = new ChangeMachineSpy(_changeMachineFake);

        if (_brewerSpy is null)
        {
            _brewerSpy = new BrewerSpy(new BrewerDummy());
            _brewer = new BrewerDummy();
        }

        var instance = new SoftwareMachineClass(_brewer, _changeMachineSpy, _coinInMachine);
        return (instance, _brewer, _changeMachineSpy, _changeMachineFake, _changeMachineSpy, _brewerSpy!, _coinInMachine);
    }

    public (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine) BuildNoCoin()
    {
        _changeMachineSpy ??= new ChangeMachineSpy(_changeMachineFake);

        if (_brewerSpy is null)
        {
            _brewerSpy = new BrewerSpy(new BrewerStub());
            _brewer = _brewerSpy;
        }

        _coinInMachine = new Dictionary<CoinCode, ushort>
        {
            { CoinCode.FiveCents, 0 },
            { CoinCode.TenCents, 0 },
            { CoinCode.TwentyCents, 0 },
            { CoinCode.FiftyCents, 0 },
            { CoinCode.OneEuro, 0 },
            { CoinCode.TwoEuros, 0 }
        };

        var instance = new SoftwareMachineClass(_brewer, _changeMachineSpy, _coinInMachine);
        return (instance, _brewer, _changeMachineSpy, _changeMachineFake, _changeMachineSpy, _brewerSpy!, _coinInMachine);
    }

    private (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy, Dictionary<CoinCode, ushort> CoinInMachine) BuildSpecifique(CoinCode code, ushort quantite)
    {
        _changeMachineSpy ??= new ChangeMachineSpy(_changeMachineFake);
        _brewerSpy ??= new BrewerSpy(new BrewerStub());
        _brewer = _brewerSpy;

        _coinInMachine = new Dictionary<CoinCode, ushort>
    {
        { CoinCode.FiveCents, 0 }, { CoinCode.TenCents, 0 }, { CoinCode.TwentyCents, 0 },
        { CoinCode.FiftyCents, 0 }, { CoinCode.OneEuro, 0 }, { CoinCode.TwoEuros, 0 }
    };
        _coinInMachine[code] = quantite;

        var instance = new SoftwareMachineClass(_brewer, _changeMachineSpy, _coinInMachine);
        return (instance, _brewer, _changeMachineSpy, _changeMachineFake, _changeMachineSpy, _brewerSpy!, _coinInMachine);
    }
}
