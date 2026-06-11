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

        if (_valueCoinInMachine < PRIX_CAFE)
        {
            if (_nbCoinInMachine == 5)
            {
                _changeMachine.FlushStoredMoney();
                _nbCoinInMachine = 0;
            }
            return;
        }

        try
        {
            _brewer.MakeACoffee();
            _changeMachine.CollectStoredMoney();
        }
        catch
        {
            _changeMachine.FlushStoredMoney();
        }
        finally
        {
            _valueCoinInMachine = 0;
            _nbCoinInMachine = 0;
        }
    }


}
