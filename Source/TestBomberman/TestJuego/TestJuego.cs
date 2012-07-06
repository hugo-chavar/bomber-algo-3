using NUnit.Framework;
using BombermanModel.Juego;
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Personaje;
using BombermanModel.Arma;
using BombermanModel;
using BombermanModel.Articulo;

namespace TestBombermanModel.TestJuego
{
    [TestFixture]
    public class TestJuego
    {
        private Juego unJuego;
        private const int ANCHOMAPA = 17;
        private const int ALTOMAPA = 13;
        private Tablero unMapa;
        private IMovible movil;

        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unJuego.ComenzarDesdeElPrincipio();
            this.unJuego.SeleccionarMapa();
            this.unJuego.CargarMapa();
            this.unMapa = this.unJuego.Ambiente;
        }


        [TearDown]
        public void TearDown()
        {
            this.unJuego.ComenzarDesdeElPrincipio();
            this.unJuego.SeleccionarMapa();
            this.unJuego.CargarMapa();
        }

        [Test]
        public void DosReferenciasAJuegoDebenSerElMismoObjeto()
        {
            //verifico que funcione el Singleton
            Juego otroJuego = Juego.Instancia();
            Assert.AreSame(otroJuego, this.unJuego);
        }

        [Test]
        public void PerderVidaDescuentaUnaVidaAlJuegoActual()
        {
            this.unJuego.ComenzarDesdeElPrincipio();
            unJuego.PerderVida();
            Assert.AreEqual(2, unJuego.CantDeVidas);
        }

        [Test]
        public void BombitaConVelocidadNormalCambiaDePosicionEnUnaUnidadDentroDelMapaAlMoversePorPasillo()
        {
            this.movil = new Bombita(new Punto(0, 0));
            Punto posOriginal = this.movil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(this.movil);
            this.movil.Movimiento.CambiarADerecha();
            this.movil.Mover();
            Punto pos = new Punto(1, 0);
            Assert.IsTrue(pos.Equals(this.movil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnPasilloSuPosicionNoEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            Assert.IsFalse(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void UnMovilDespuesDeMoverseEnDireccionAUnObstaculoSuPosicionEsIgualALaOriginal()
        {
            IMovible otroMovil = new Bombita(new Punto(1, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil); 
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();
            Assert.IsTrue(posOriginal.Equals(otroMovil.Posicion));
        }

        [Test]
        public void BombitaChocaConObstaculoMoverNoCambiaSuPosicion()
        {
            IMovible otroMovil = Juego.Instancia().Protagonista;
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();
            otroMovil.Mover();
            Punto posObstaculo = new Punto(2, 0);
            Assert.IsFalse(otroMovil.Posicion.Equals(posObstaculo));
        }

        [Test]
        public void BombitaAvanzaPorTodoElMapaYNoCambiaDePosCuandoChocaConElLimiteDerechoDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(10, 1));
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover();//fue a 10,0
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();//fue a 11,0
            otroMovil.Mover();//fue a 12,0
            otroMovil.Mover();//fue a 13,0
            otroMovil.Mover();//fue a 14,0
            otroMovil.Mover();//fue a 15,0
            otroMovil.Mover();//fue a 16,0, esta en el limite!
            otroMovil.Mover(); //choca con el limite
            Punto posFinal = new Punto(ANCHOMAPA -1, 0);
            Assert.IsTrue(otroMovil.Posicion.Equals(posFinal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteIzquierdoDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(0, 2));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAIzquierda();
            otroMovil.Mover(); //choca con el limite izquierdo
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteInferiorDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(3, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover(); //choca con el limite inferior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void BombitaNoCambiaDePosCuandoChocaConElLimiteSuperiorDelMapa()
        {
            IMovible otroMovil = new Bombita(new Punto(2, ALTOMAPA-1));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //choca con el limite superior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoAvanzaSobreUnObstaculoSuPosicionEsDistintaALaOriginal()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(1, 0));
            Punto posOriginal = otroMovil.Posicion.Clonar();
            this.unMapa.AgregarPersonaje(otroMovil);
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover(); //se mueve donde hay un bloque de acero
            Assert.IsFalse(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoAtraviesaObstaculos()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil);
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

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteIzquierdoDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(0, 0));
            this.unMapa.AgregarPersonaje(otroMovil);
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAIzquierda();
            otroMovil.Mover();//choca con el limite izquierdo
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteDerechoDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(ANCHOMAPA-1, 0));
            this.unMapa.AgregarPersonaje(otroMovil);
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarADerecha();
            otroMovil.Mover();//choca con el limite derecho
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteSuperiorDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(4, ALTOMAPA-1));
            this.unMapa.AgregarPersonaje(otroMovil);
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAArriba();
            otroMovil.Mover();//choca con el limite superior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void UnLopezReggaeAladoNoAtraviesaElLimiteInferiorDelMapa()
        {
            IMovible otroMovil = new LosLopezReggaeAlado(new Punto(3, 0));
            this.unMapa.AgregarPersonaje(otroMovil);
            Punto posOriginal = otroMovil.Posicion.Clonar();
            otroMovil.Movimiento.CambiarAAbajo();
            otroMovil.Mover();//choca con el limite inferior
            Assert.IsTrue(otroMovil.Posicion.Equals(posOriginal));
        }

        [Test]
        public void CuandoExplotaUnaBombaToleToleYTieneUnCasilleroAbajoConCecilioLoDestruye()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionCecilio = new Punto(2, 2);
            Cecilio cecilio = new Cecilio(posicionCecilio);

            Casilla casillaCecilio = unMapa.ObtenerCasilla(posicionCecilio);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaToleTole(posicionBomba, 0);
            casillaCecilio.Transitar(cecilio);

            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(cecilio.UnidadesDeResistencia, 0);
        }

        [Test]
        public void CuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConCecilioLoDestruye()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionCecilio = new Punto(2, 2);
            Cecilio cecilio = new Cecilio(posicionCecilio);

            Casilla casillaCecilio = unMapa.ObtenerCasilla(posicionCecilio);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaMolotov(posicionBomba, 0);
            casillaCecilio.Transitar(cecilio);

            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(cecilio.UnidadesDeResistencia, 0);
        }

        [Test]
        public void CuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConLopezReggaeLoDania5Unidades()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionLopezReggae = new Punto(2, 2);
            LosLopezReggae lopezReggae = new LosLopezReggae(posicionLopezReggae);

            Casilla casillaLopez = unMapa.ObtenerCasilla(posicionLopezReggae);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaMolotov(posicionBomba, 0);
            casillaLopez.Transitar(lopezReggae);
            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(lopezReggae.UnidadesDeResistencia, 5);
        }

        [Test]
        public void CuandoExplotaUnaBombaMolotovYTieneUnCasilleroAbajoConLopezReggaeAladoLoDestruye()
        {
            Punto posicionBomba = new Punto(2, 3);
            Punto posicionAlado = new Punto(2, 2);
            LosLopezReggaeAlado alado = new LosLopezReggaeAlado(posicionAlado);

            Casilla casillaAlado = unMapa.ObtenerCasilla(posicionAlado);
            Casilla casillaBomba = unMapa.ObtenerCasilla(posicionBomba);

            Bomba unaBomba = new BombaMolotov(posicionBomba, 0);
            casillaAlado.Transitar(alado);
            casillaBomba.PlantarExplosivo(unaBomba);
            unaBomba.Explotar();
            Assert.AreEqual(alado.UnidadesDeResistencia, 0);
        }

        [Test]
        public void CuandoHagoExplotar2BombasAlMismoTiempoATravesDeJuegoDebenExplotar()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaMolotov bomba2 = new BombaMolotov(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);
            Juego.Instancia().AlojarExplosivo(bomba1);
            Juego.Instancia().AlojarExplosivo(bomba2);
            System.Threading.Thread.Sleep(3000);
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsTrue(bomba2.EstaExplotado());
        }

        [Test]
        public void CuandoPlanto1BombaToleTole1MolotovAlMismoTiempoATravesDeJuegoYSoloExplotaLaMolotov()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaToleTole bomba2 = new BombaToleTole(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            this.unJuego.AlojarExplosivo(bomba1);
            this.unJuego.AlojarExplosivo(bomba2);
            System.Threading.Thread.Sleep(3000);
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsFalse(bomba2.EstaExplotado());
        }

        [Test]
        public void CuandoPlanto1BombaToleTole1MolotovAlMismoTiempoATravesDeJuegoYExplotanLas2()
        {
            Punto posicionBomba1 = new Punto(0, 0);
            Punto posicionBomba2 = new Punto(0, 1);

            BombaMolotov bomba1 = new BombaMolotov(posicionBomba1, 0);
            BombaToleTole bomba2 = new BombaToleTole(posicionBomba2, 0);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba1);
            Casilla casillaBomba2 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBomba2);

            Juego.Instancia().AlojarExplosivo(bomba1);
            Juego.Instancia().AlojarExplosivo(bomba2);

            System.Threading.Thread.Sleep(5000);
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();

            Assert.IsTrue(bomba1.EstaExplotado());
            Assert.IsTrue(bomba2.EstaExplotado());
            Assert.AreEqual(Juego.Instancia().DependientesDelTiempo.Count, 0);
        }

        [Test]
        public void AlLanzarUnaBombaLaDebeAgregarEnListaDeLosQueEsperanParaExplotar()
        {

            Punto posicionBombita1 = new Punto(3, 0);
            int cantDependeTiempo = Juego.Instancia().DependientesDelTiempo.Count;
            Bombita movil = new Bombita(posicionBombita1);

            Casilla casillaBomba1 = Juego.Instancia().Ambiente.ObtenerCasilla(posicionBombita1);
            casillaBomba1.Transitar(movil);
            
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();
            movil.LanzarExplosivo();

            Assert.AreEqual((cantDependeTiempo+1), Juego.Instancia().DependientesDelTiempo.Count);
        }

        [Test] 
        public void CuandoGeneroUnMapaNuevoYAgregoAUnEnemigoLaCantidadDeEnemigosAumentaAen1()
        {

            int enemigosVivos = this.unJuego.CantidadEnemigosVivos();
            Punto pCecilio = new Punto(0, 1);

            Cecilio unCecilio = new Cecilio(pCecilio);

            this.unJuego.AgregarEnemigo(unCecilio);

            Assert.AreEqual(this.unJuego.CantidadEnemigosVivos(), enemigosVivos+1);

        }

        [Test] 
        public void CuandoGeneroUnMapaNuevoYElimino1EnemigoConToleToleRestaUnEnemigoVivo()
        {
            int cantEnemigos = this.unJuego.CantidadEnemigosVivos();
            Punto p = new Punto(2, 2);
            Personaje bombita = new Bombita(p);
            this.unJuego.Ambiente.AgregarPersonaje(bombita);
            Cecilio unCeci = new Cecilio(p);
            this.unJuego.AgregarEnemigo(unCeci);
            bombita.CambiarLanzadorAToleTole();
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Mover();
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            System.Threading.Thread.Sleep(5000);//Pasan 5 segundos
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();

            Assert.AreEqual(this.unJuego.CantidadEnemigosVivos(), cantEnemigos);

        }

    }
}
