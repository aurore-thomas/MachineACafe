using SoftwareMachine;
using Xunit;
using Assert = Xunit.Assert;

namespace TestMachineACafe
{
    public class Tests
    {
        [Fact(DisplayName = "Quand la bonne somme est insérée, un café est servi")]
        public void CasNominal()
        {
            const ushort prixCafeEnCours = 40;

            // Etant donné une machine à café
            var machineACafe = new SoftwareMachineClass();

            // Quand le hardware signale une somme suffisante pour le prix d'un café
            machineACafe.InsérerPiece(prixCafeEnCours);

            // ALORS le hardware est sollicité pour faire couler un café
            Assert.Equal(1, machineACafe.NombreCafeServi);

            // ET il est demandé au hardware de collecter les fonds
            Assert.Equal(prixCafeEnCours, machineACafe.SommeEncaisseEnCentimes);

        }

        [Fact(DisplayName = "Quand la bonne somme est insérée 2 fois, deux café sont servis")]
        public void Cas2Cafes()
        {
            const ushort prixCafeEnCours = 40;

            // Etant donné une machine à café
            var machineACafe = new SoftwareMachineClass();

            // Quand le hardware signale une somme suffisante pour le prix de deux cafés
            machineACafe.InsérerPiece(prixCafeEnCours);
            machineACafe.InsérerPiece(prixCafeEnCours);

            // ALORS le hardware est sollicité pour faire couler deux café
            Assert.Equal(2, machineACafe.NombreCafeServi);

            // ET il est demandé au hardware de collecter les fonds
            Assert.Equal(prixCafeEnCours * 2, machineACafe.SommeEncaisseEnCentimes);

        }

        //[Fact(DisplayName = "Quand la somme insérée est supérieure au montant du café, le surplus est rendu")]
        //public void CasTropDeMonnaie()
        //{
        //    const ushort prixCafeEnCours = 20;
        //    const ushort montantInsere = 50;

        //    // Etant donné une machine à café
        //    var machineACafe = new SoftwareMachineClass();

        //    // Quand le hardware signale une somme supérieure pour le prix d'un café
        //    machineACafe.InsérerPiece(prixCafeEnCours, montantInsere);

        //    // ALORS le hardware est sollicité pour rendre le surplus
        //    Assert.Equal(30, machineACafe.SurplusMonnaie);

        //    // ET il est demandé au hardware de servir un café
        //    Assert.Equal(1, machineACafe.NombreCafeServi);

        //    // ET il est demandé au hardware de collecter les fonds
        //    Assert.Equal(prixCafeEnCours, machineACafe.SommeEncaisseEnCentimes);
        //}

        //[Fact(DisplayName = "Quand l'utilisateur annule sa commande après avoir inséré des pièces")]
        //public void CasAnnulation()
        //{
        //    const ushort montantInseree = 80;

        //    // Etant donné une machine à café
        //    var machineACafe = new SoftwareMachineClass();

        //    // Quand le hardware signale une annulation de commande après insertion de pièces
        //    machineACafe.InsérerPiece(0, montantInseree);
        //    machineACafe.AnnulerCommande(0, montantInseree);

        //    // Alors le hardware est sollicité pour rendre la monnaie insérée
        //    Assert.Equal(80, machineACafe.SurplusMonnaie);

        //    // ET il est demandé au hardware de ne pas prendre en compte cette monnaie dans son total cumulé
        //    Assert.Equal(0, machineACafe.SommeEncaisseEnCentimes);
        //}


        //[Fact(DisplayName = "Quand on insert la pièce, celle-ci est reconnue par la machine")]
        //public void VerifyPiece()
        //{

        //    // Etant donne une machine a cafe 
        //    SoftwareMachineClass machineAcafe = new SoftwareMachineClass();

        //    // Quand il détecte la pièce
        //    bool piece20 = machineAcafe.VerifierPiece(20);
        //    bool piece1 = machineAcafe.VerifierPiece(1);

        //    //alors  il accepte les pieces de 20 centimes
        //    Assert.True(piece20);
        //    //et il accepte pas les pieces de 1 centime
        //    Assert.False(piece1);
        //}
    }
}
