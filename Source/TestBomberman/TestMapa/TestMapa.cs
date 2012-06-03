using NUnit;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;

namespace TestBomberman.TestMapa
{
    [TestFixture]
    class TestMapa
    {
        private Mapa unMapa;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            //creo un mapa 5x5 con esta distribucion (P = Pasillo, * = BloqueAcero):
            /*
             *      P P P P P
             *      P * P * P
             *      P P P P P
             *      P * P * P
             *      P P P P P
             */
            
            Punto unaPosicion;
            Casilla unaCasilla;
            this.unMapa = new Mapa(5,5);
            FabricaDeCasillas unaFabricaDeCasillas = new FabricaDeCasillas();

            unaPosicion = new Punto(0, 0);
            unaCasilla = unaFabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
            this.unMapa.agregarCasilla(unaCasilla);

            /* int i,j;
             * for (i=0;i<5;i++)
                for (j = 0; j < 5; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 0 && (j & 1) == 0)
                    {
                        //ambos son numeros pares
                        unaCasilla = unaFabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    { 
                        //uno de los dos es impar
                        unaCasilla = unaFabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }

                    this.unMapa.agregarCasilla(unaCasilla);
                }*/

        }

    }
}
