using Hardware;

namespace MachineACafé;


public class SoftwareMachineClass
{
    private readonly IBrewer _brewer;
    private readonly IChangeMachine _changeMachine;
    private ushort _nbCoinInMachine;
    private ushort _valueCoinInMachine;
    private const ushort PRIX_CAFE = 40;

    private Dictionary<CoinCode, ushort> _nbCoinInMachineByCoinCode = new Dictionary<CoinCode, ushort>();

    public SoftwareMachineClass(IBrewer brewer, IChangeMachine changeMachine, Dictionary<CoinCode, ushort> nbCoinInMachineByCoinCode)
    {
        _brewer = brewer;
        _changeMachine = changeMachine;
        _changeMachine.RegisterMoneyInsertedCallback(coin => Insérer(new Coin((ushort)coin)));
        _nbCoinInMachineByCoinCode = nbCoinInMachineByCoinCode;
    }

    private void Insérer(Coin somme)
    {
        _valueCoinInMachine += somme.ValueInCents;
        _nbCoinInMachine++;

        CoinCode coinCode = somme.GetCoinCode();

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

            if (_valueCoinInMachine > PRIX_CAFE)
            {
                var valueToReturn = (ushort)(_valueCoinInMachine - PRIX_CAFE);

                var piecesDisponiblesTriees = _nbCoinInMachineByCoinCode
                    .Select(kvp => new { Code = kvp.Key, Valeur = somme.GetValue(kvp.Key) })
                    .OrderByDescending(p => p.Valeur)
                    .ToList();

                foreach (var piece in piecesDisponiblesTriees)
                {
                    if (valueToReturn == 0) break;

                    while (valueToReturn >= piece.Valeur && _nbCoinInMachineByCoinCode[piece.Code] > 0)
                    {
                        _nbCoinInMachineByCoinCode[piece.Code]--;
                        valueToReturn -= piece.Valeur;

                        _changeMachine.FlushStoredMoney();
                    }
                }

                //_changeMachine.FlushStoredMoney();
            }
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
