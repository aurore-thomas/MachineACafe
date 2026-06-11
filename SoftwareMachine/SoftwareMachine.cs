using Hardware;

namespace MachineACafé;


public class SoftwareMachineClass
{
    private readonly IBrewer _brewer;
    private readonly IChangeMachine _changeMachine;
    private ushort _nbCoinInMachine;
    private ushort _valueCoinInMachine;
    private const ushort PRIX_CAFE = 40;

    public SoftwareMachineClass(IBrewer brewer, IChangeMachine changeMachine)
    {
        _brewer = brewer;
        _changeMachine = changeMachine;
        _changeMachine.RegisterMoneyInsertedCallback(coin => Insérer(new Coin((ushort)coin)));
    }

    private void Insérer(Coin somme)
    {
        _valueCoinInMachine += somme.ValueInCents;
        _nbCoinInMachine++;

        if (_valueCoinInMachine < PRIX_CAFE
            && _nbCoinInMachine == 5)
        {
            _changeMachine.FlushStoredMoney();
            _nbCoinInMachine = 0;
            return;
        }
        else if (_valueCoinInMachine < PRIX_CAFE
            && _nbCoinInMachine < 5)
        {
            return;
        }

        try
        {
            _brewer.MakeACoffee();
            _changeMachine.CollectStoredMoney();
            _valueCoinInMachine = 0;
            _nbCoinInMachine = 0;
        }
        catch
        {
            _changeMachine.FlushStoredMoney();
        }
    }


}
