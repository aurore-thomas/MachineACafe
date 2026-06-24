using Hardware;

namespace MachineACafé;

public record Coin
{
    private readonly HashSet<ushort> _validValues = [1, 2, 5, 10, 20, 50, 100, 200];

    public Coin(ushort valueInCents)
    {
        if (!_validValues.Contains(valueInCents))
            throw new ArgumentOutOfRangeException(nameof(valueInCents));

        ValueInCents = valueInCents;
    }

    public ushort ValueInCents { get; }

    public CoinCode GetCoinCode()
    {
        return ValueInCents switch
        {
            5 => CoinCode.FiveCents,
            10 => CoinCode.TenCents,
            20 => CoinCode.TwentyCents,
            50 => CoinCode.FiftyCents,
            100 => CoinCode.OneEuro,
            200 => CoinCode.TwoEuros,
            _ => throw new InvalidOperationException("Invalid coin value")
        };
    }

    public ushort GetValue(CoinCode code)
    {
        return code switch
        {
            CoinCode.TwoEuros => 200,
            CoinCode.OneEuro => 100,
            CoinCode.FiftyCents => 50,
            CoinCode.TwentyCents => 20,
            CoinCode.TenCents => 10,
            CoinCode.FiveCents => 5,
            _ => 0
        };
    }
}