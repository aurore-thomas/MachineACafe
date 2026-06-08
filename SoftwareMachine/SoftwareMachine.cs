using Hardware;

namespace SoftwareMachine
{
    public class SoftwareMachineClass
    {
        public void InsérerPiece(ushort montantEnCents)
        {
            if (montantEnCents != 40) return;

            NombreCafeServi++;
            SommeEncaisseEnCentimes += 40;
        }

        //public void InsérerPiece(ushort montantEnCents, ushort montantInsere)
        //{
        //    NombreCafeServi++;
        //    SommeEncaisseEnCentimes += montantEnCents;
        //    SurplusMonnaie = (ushort)(montantInsere - montantEnCents);
        //}

        //public void AnnulerCommande(ushort montantCafe, ushort montantInsere)
        //{
        //    NombreCafeServi --;
        //    SommeEncaisseEnCentimes -= montantCafe;
        //    SurplusMonnaie = montantInsere;
        //}

        //public bool VerifierPiece(ushort montantEnCents)
        //{
        //    return Enum.IsDefined(typeof(CoinCode), (int)montantEnCents);
        //}

        public ushort NombreCafeServi { get; private set; }
        public ushort SommeEncaisseEnCentimes { get; private set; }
        //public ushort SurplusMonnaie { get; private set; }


    }
}
