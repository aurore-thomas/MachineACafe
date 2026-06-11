using Hardware;

namespace MachineACafé.Test.Utilities;

public class ChangeMachineFake : IChangeMachine
{
    private Action<CoinCode>? _callback;

    public void RegisterMoneyInsertedCallback(Action<CoinCode> callback)
    {
        if (_callback != null) throw new NotSupportedException();
        _callback = callback;
    }

    public void FlushStoredMoney()
    {
    }

    public void CollectStoredMoney()
    {
    }

    public bool DropCashback(CoinCode coinCode)
    {
        throw new NotImplementedException();
    }

    public void SimulerInsertionPièce(CoinCode fiftyCents)
    {
        _callback?.Invoke(fiftyCents);
    }
}