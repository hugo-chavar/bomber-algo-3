using NUnit.Framework;
using Bomberman.Juego;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;
using Bomberman;

namespace TestBomberman.TestJuego
{
    [TestFixture]
    public class TestJuego
    {
        private Juego unJuego;

        private const int ANCHOMAPA = 5;
        private const int ALTOMAPA = 5;
        private Mapa unMapa;
        //private Mapa otroMapa;
        //private Casilla unaCasilla;
        private IMovible movil;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            //creo un mapa 5x5 con esta distribucion (P = Pasillo, * = BloqueAcero):
            //      P P P P P
            //      P * P * P
            //      P P P P P
            //      P * P * P
            //      P P P P P

            Punto unaPosicion;
            Casilla unaCasilla;
            this.unMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);
            
            int i, j;
            for (i = 0; i < ANCHOMAPA; i++)
                for (j = 0; j < ANCHOMAPA; j++)
                {
                    unaPosicion = new Punto(i, j);
                    if ((i & 1) == 1 && (j & 1) == 1)
                    {
                        //ambos son numeros impares
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    {
                        //uno de los dos es par
                        unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }
                    this.unMapa.AgregarCasilla(unaCasilla);
                }
            
            this.unJuego = Juego.Instancia();
            this.unJuego.Ambiente = this.unMapa;

        }

        [Test]
        public void DosReferenciasAJuegoDebenSerElMismoObjeto()
        {
            //verifico que funcione el Singleton
            Juego otroJuego = Juego.Instancia();
            Assert.AreSame(otroJuego, this.unJuego);
        }

        [Test]
        public void PausarJuegoFuncionaOK() // buuuuaaaaaaaa!! nos van a bochar por los nombres de los tests
        {
            unJuego.PausarJuego();
            Assert.AreEqual(unJuego.JuegoPausado, true);//wtf!!! usar Assert.IsTrue(unJuego.JuegoPausado);
        }

        [Test]
        public void DesPausarJuegoFuncionaOK() // no pegamos una con los nombres che!
        {
            unJuego.PausarJuego();
            unJuego.DesPausarJuego();
            Assert.AreEqual(unJuego.JuegoPausado, false); //aijuna!!! usar Assert.IsFalse(unJuego.JuegoPausado);
        }

        [Test]
        public void PerderVidaFuncionaOK() // che!! porque usan estos nombres!!! usar algo así PerderVidaDescuentaUnaVidaAlJuegoActual()!!!!!
        {
            unJuego.PerderVida();
            Assert.AreEqual(2, unJuego.CantDeVidas);
        }

        [Test]
        public void BombitaCambiaDePosicionEnUnaUnidadDentroDelMapaAlMoversePorPasillo()
        {
            this.movil = new Bombita(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(this.movil); // testear todos los exceptions
            this.movil.Movimiento.CambiarADerecha();
            this.movil.Mover();
            Punto pos = new Punto(1,0);
            Assert.IsTrue(pos.Equals(this.movil.Posicion));
        }


        [Test]
        public void BombitaChocaConObstaculoMoverNoCambiaSuPosicion()
        {
            this.movil = new Bombita(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(this.movil); // testear todos los exceptions
            this.movil.Movimiento.CambiarADerecha();
            this.movil.Mover();
            this.movil.Movimiento.CambiarAIzquierda();
            this.movil.Mover();
            Punto posOriginal = this.movil.Posicion.Clonar();
            Punto posFrenteSuyo = this.movil.Posicion.PosicionSuperior();
            Punto posTest = new Punto(1, 1);
            Assert.IsTrue(posTest.Equals(posFrenteSuyo));
        }
    }
}
