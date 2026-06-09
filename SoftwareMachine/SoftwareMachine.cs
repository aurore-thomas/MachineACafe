using Hardware;

namespace SoftwareMachine
{
    public class SoftwareMachineClass
    {
        public const ushort PrixDuCafeEnCentimes = 40;

        private readonly IBrewer _brewer;
        private readonly IChangeMachine _changeMachine;

        public SoftwareMachineClass(IBrewer brewer, IChangeMachine changeMachine)
        {
            _brewer = brewer;
            _changeMachine = changeMachine;
        }

        public void InsererPiece(ushort montantEnCents)
        {
            try
            {
                if (montantEnCents < PrixDuCafeEnCentimes) throw new Exception();

                //NombreCafeServi++;
                //SommeEncaisseEnCentimes += montantEnCents;

                _brewer.MakeACoffee();
                _changeMachine.CollectStoredMoney();
            }
            catch {
                _changeMachine.FlushStoredMoney();
            }
        }

        public ushort NombreCafeServi { get; private set; }
        public ushort SommeEncaisseEnCentimes { get; private set; }


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

        //public ushort SurplusMonnaie { get; private set; }


    }
}
