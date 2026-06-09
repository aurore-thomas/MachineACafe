using Hardware;
using SoftwareMachine;

namespace MachineACafe.Test.Utilitaires;

internal class ChangeMachineSpy : IChangeMachine
{
    private readonly IChangeMachine _behavior;

    public ushort FlushStoredMoneyInvocations { get; private set; }
    public ushort CollectStoredMoneyInvocations { get; private set; }

    public ChangeMachineSpy(IChangeMachine behavior)
    {
        _behavior = behavior;
    }

    public void CollectStoredMoney()
    {
        CollectStoredMoneyInvocations += SoftwareMachineClass.PrixDuCafeEnCentimes;
        _behavior.CollectStoredMoney();
    }

    public bool DropCashback(CoinCode coinCode)
    {
        return _behavior.DropCashback(coinCode);
    }

    public void FlushStoredMoney()
    {
        FlushStoredMoneyInvocations++;
        _behavior.FlushStoredMoney();
    }

    public void RegisterMoneyInsertedCallback(Action<CoinCode> callback)
    {
        _behavior.RegisterMoneyInsertedCallback(callback);
    }
}
