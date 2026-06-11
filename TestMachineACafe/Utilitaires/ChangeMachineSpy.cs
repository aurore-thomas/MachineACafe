using Hardware;

namespace MachineACafé.Test.Utilities;

internal class ChangeMachineSpy : IChangeMachine
{
    private readonly IChangeMachine _behavior;

    public ushort FlushStoredMoneyInvocations { get; private set; }
    public ushort CollectStoredMoneyInvocations { get; private set; }
    public bool Untouched => FlushStoredMoneyInvocations == 0 && CollectStoredMoneyInvocations == 0;

    public ChangeMachineSpy() : this(new ChangeMachineStub())
    { }

    public ChangeMachineSpy(IChangeMachine behavior)
    {
        _behavior = behavior;
    }

    public void RegisterMoneyInsertedCallback(Action<CoinCode> callback)
    {
        _behavior.RegisterMoneyInsertedCallback(callback);
    }

    public void FlushStoredMoney()
    {
        FlushStoredMoneyInvocations++;
        _behavior.FlushStoredMoney();
    }

    public void CollectStoredMoney()
    {
        CollectStoredMoneyInvocations++;
        _behavior.CollectStoredMoney();
    }

    public bool DropCashback(CoinCode coinCode)
    {
        return _behavior.DropCashback(coinCode);
    }
}