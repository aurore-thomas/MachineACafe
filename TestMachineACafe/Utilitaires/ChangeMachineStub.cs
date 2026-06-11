using Hardware;

namespace MachineACafé.Test.Utilities;

internal class ChangeMachineStub : IChangeMachine
{
    public void RegisterMoneyInsertedCallback(Action<CoinCode> callback)
    {
    }

    public void FlushStoredMoney()
    {
    }

    public void CollectStoredMoney()
    {
    }

    public bool DropCashback(CoinCode coinCode)
    {
        return false;
    }
}