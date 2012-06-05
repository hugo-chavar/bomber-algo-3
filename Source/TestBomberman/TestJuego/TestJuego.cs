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
        public void BombitaConVelocidadNormalCambiaDePosicionEnUnaUnidadDentroDelMapaAlMoversePorPasillo()
        {
            this.movil = new Bombita(new Punto(0, 0));
            Punto posOriginal = this.movil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(this.movil); // testear todos los exceptions
            this.movil.Movimiento.CambiarADerecha();
            this.movil.Mover();
            Punto pos = new Punto(1,0);
            Assert.IsTrue(pos.Equals(this.movil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnPasilloSuPosicionNoEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            //Punto pos = new Punto(1, 0);
            Assert.IsFalse(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnObstaculoSuPosicionEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(1, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();
            Assert.IsTrue(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void BombitaChocaConObstaculoMoverNoCambiaSuPosicion()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            //Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();
            Punto posObstaculo = new Punto(1, 1);
            Assert.IsFalse(otroMovil.Posicion.Equals(posObstaculo));
        }

        [Test]
        public void BombitaAvanzaPorTodoElMapaYNoCambiaDePosCuandoChocaConElLimiteDerechoDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            //Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();//fue a 0,1
            otroMovil.Mover();//fue a 0,2
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover(); //fue a 1,2
            otroMovil.Mover();//fue a 2,2
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //fue a 2,3
            otroMovil.Mover();//fue a 2,4
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover(); //fue a 3,4
            otroMovil.Mover();//fue a 4,4, esta en el limite!
            otroMovil.Mover(); //choca con el limite
            Punto posFinal = new Punto(4, 4);
            Assert.IsTrue(otroMovil.Posicion.Equals(posFinal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteIzquierdoDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 2));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarAIzquierda();
            otroMovil.Mover(); //choca con el limite izquierdo
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteInferiorDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(3, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover(); //choca con el limite inferior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteSuperiorDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(2, 4));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //choca con el limite superior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoAvanzaSobreUnObstaculoSuPosicionEsDistintaALaOriginal()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(1, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //se mueve donde hay un bloque de acero
            Assert.IsFalse(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoAtraviezaObstaculos()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil); // testear todos los exceptions
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();//fue a 0,1
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();//fue a 1,1 (esta sobre un bloque de acero)
            otroMovil.Mover(); //fue a 2,1 (esta sobre pasillo)
            otroMovil.Mover();//fue a 3,1 (esta sobre un bloque de acero)
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //fue a 3,2 (esta sobre pasillo)
            otroMovil.Mover();//fue a 3,3 (esta sobre un bloque de acero)
            otroMovil.Mover(); //fue a 3,4
            Punto posFinal = new Punto(3, 4);
            Assert.IsTrue(otroMovil.Posicion.Equals(posFinal));
        }

    }
}
