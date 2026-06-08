namespace SoftwareMachine
{
    public class SoftwareMachineClass
    {
        public void InsérerPiece(ushort montantEnCents)
        {
            NombreCafeServi++;
            SommeEncaisseEnCentimes += 40;
        }

        public ushort NombreCafeServi { get; private set; }
        public ushort SommeEncaisseEnCentimes { get; private set; }

    }
}
