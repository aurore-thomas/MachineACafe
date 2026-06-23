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

    // Retourne aussi le fake et le spy pour les tests qui en ont besoin.
    public static (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy)
        Default => new SoftwareMachineBuilder().Build();

    public static (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy)
        Defaillant => new SoftwareMachineBuilder().BuildDefaillant();

    public (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy) Build()
    {
        _changeMachineSpy ??= new ChangeMachineSpy(_changeMachineFake);

        if (_brewerSpy is null)
        {
            _brewerSpy = new BrewerSpy(new BrewerStub());
            _brewer = _brewerSpy;
        }

        var instance = new SoftwareMachineClass(_brewer, _changeMachineSpy);
        return (instance, _brewer, _changeMachineSpy, _changeMachineFake, _changeMachineSpy, _brewerSpy!);
    }

    public (SoftwareMachineClass Instance, IBrewer Brewer, IChangeMachine ChangeMachine, ChangeMachineFake ChangeMachineFake, ChangeMachineSpy ChangeMachineSpy, BrewerSpy BrewerSpy) BuildDefaillant()
    {
        _changeMachineSpy = new ChangeMachineSpy(_changeMachineFake);

        if (_brewerSpy is null)
        {
            _brewerSpy = new BrewerSpy(new BrewerDummy());
            _brewer = new BrewerDummy();
        }

        var instance = new SoftwareMachineClass(_brewer, _changeMachineSpy);
        return (instance, _brewer, _changeMachineSpy, _changeMachineFake, _changeMachineSpy, _brewerSpy!);
    }
}
