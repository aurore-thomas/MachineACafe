using Hardware;

namespace MachineACafé;

public class SoftwareMachineClass
{
    private readonly IBrewer _brewer;
    private readonly IChangeMachine _changeMachine;

    public SoftwareMachineClass(IBrewer brewer, IChangeMachine changeMachine)
    {
        _brewer = brewer;
        _changeMachine = changeMachine;
        _changeMachine.RegisterMoneyInsertedCallback(coin => Insérer(new Coin((ushort)coin)));
    }

    private void Insérer(Coin somme)
    {
        if (somme.ValueInCents < 40)
        {
            _changeMachine.FlushStoredMoney();
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
    }


}
