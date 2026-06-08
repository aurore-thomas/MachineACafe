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

            // Quand le hardware signale une somme insuffisante pour le prix d'un café
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

            // Quand le hardware signale une somme insuffisante pour le prix d'un café
            machineACafe.InsérerPiece(prixCafeEnCours);
            machineACafe.InsérerPiece(prixCafeEnCours);

            // ALORS le hardware est sollicité pour faire couler un café
            Assert.Equal(2, machineACafe.NombreCafeServi);

            // ET il est demandé au hardware de collecter les fonds
            Assert.Equal(prixCafeEnCours * 2, machineACafe.SommeEncaisseEnCentimes);

        }
    }
}
