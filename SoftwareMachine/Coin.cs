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
}