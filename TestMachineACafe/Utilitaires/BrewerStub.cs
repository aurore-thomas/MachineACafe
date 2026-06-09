using Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineACafe.Test.Utilitaires
{
    internal class BrewerStub : IBrewer
    {
        public bool MakeACoffee()
        {
            return true;
        }

        public bool PourChocolate()
        {
            return true;
        }

        public bool PourMilk()
        {
            return true;
        }

        public bool PourSugar()
        {
            return true;
        }

        public bool PourWater()
        {
            return true;
        }

        public bool TryPullWater()
        {
            return true;
        }
    }
}
