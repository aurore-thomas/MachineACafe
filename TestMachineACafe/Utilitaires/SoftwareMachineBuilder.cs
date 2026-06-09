using Hardware;
using SoftwareMachine;

namespace MachineACafe.Test.Utilitaires;

internal class SoftwareMachineBuilder
{
    private IBrewer _brewer = new BrewerStub();
    private IChangeMachine _changeMachine = new ChangeMachineStub();

    public SoftwareMachineClass Build()
    {
        return new SoftwareMachineClass(_brewer, _changeMachine);
    }

    public SoftwareMachineBuilder AyantUnBrewer(IBrewer brewer)
    {
        _brewer = brewer;
        return this;
    }

    public SoftwareMachineBuilder AyantUnBrewerDefaillant()
    {
        _brewer = new BrewerDummy();
        return this;
    }

    public SoftwareMachineBuilder AyantUneChangeMachine(IChangeMachine changeMachine)
    {
        _changeMachine = changeMachine;
        return this;
    }
}
