using Hardware;

namespace MachineACafe.Test.Utilitaires;

internal class ChangeMachineStub : IChangeMachine
{
    //public void MakeACoffe()
    //{
    //}

    public void CollectStoredMoney()
    {
    }

    public bool DropCashback(CoinCode coinCode)
    {
        return false;
    }

    public void FlushStoredMoney()
    {
    }

    public void RegisterMoneyInsertedCallback(Action<CoinCode> callback)
    {
    }
}
