using Hardware;

namespace SoftwareMachine
{
    public class SoftwareMachineClass
    {
        public void InsérerPiece(ushort montantEnCents)
        {
            NombreCafeServi++;
            SommeEncaisseEnCentimes += 40;
        }

        public bool VerifierPiece(ushort montantEnCents)
        {
            return Enum.IsDefined(typeof(CoinCode), (int)montantEnCents);
        }

        public ushort NombreCafeServi { get; private set; }
        public ushort SommeEncaisseEnCentimes { get; private set; }

    }
}
