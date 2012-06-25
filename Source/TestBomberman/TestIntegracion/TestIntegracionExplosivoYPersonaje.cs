using NUnit.Framework;
using BombermanModel.Juego;
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Personaje;
using BombermanModel.Articulo;
using BombermanModel.Arma;
using BombermanModel;
namespace TestBombermanModel.TestIntegracion
{
    class TestIntegracionExplosivoYPersonaje
    {
        private Juego unJuego;
        private Mapa unMapa;


        [SetUp]
        public void TestSetup()
        {
            this.unJuego = Juego.Instancia();
            this.unMapa = this.unJuego.Ambiente;

        }

        [TearDown]
        public void TearDown()
        {
            Juego.Reiniciar();
        }

        [Test]
        public void BombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceYNoEsDaniadoPorLaBomba()
        {
            Punto PosicionDePlantado = new Punto(1,0);
            Bombita bombita = new Bombita(PosicionDePlantado);
            this.unJuego.Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            System.Threading.Thread.Sleep(1000);
            this.unJuego.AvanzarElTiempo();
            Assert.IsFalse(bombita.Destruido());
        }


        [Test]
        public void BombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceYLuegoDeQueLaBombaExplotaVuelveYPlantaOtraMolotov()
        {
            Punto PosicionDePlantado=new Punto(7,0);
            Bombita bombita = new Bombita(PosicionDePlantado);
            this.unJuego.Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            System.Threading.Thread.Sleep(1000);//Pasa 1 segundo
            this.unJuego.AvanzarElTiempo();
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.LanzarExplosivo();
            Assert.IsInstanceOf( typeof(BombaMolotov),Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Explosivo);
        }

        [Test]
        public void BombitaPlantaUnaMolotovSeMueveFueraDeSuAlcanceAgarraArticuloYLuegoDeQueLaBombaExplotaVuelveYPlantaUnaToleTole()
        {
            Punto PosicionDePlantado = new Punto(8, 2);
            Bombita bombita = new Bombita(PosicionDePlantado);
            Casilla unaCasilla = this.unJuego.Ambiente.ObtenerCasilla(PosicionDePlantado);
            Casilla casillaBloqueConArticulo = this.unJuego.Ambiente.ObtenerCasilla(new Punto(7, 2));
            unaCasilla.Transitar(bombita);
            bombita.LanzarExplosivo();
            bombita.Movimiento.CambiarADerecha();
            bombita.Mover();
            bombita.Mover();
            bombita.Movimiento.CambiarAArriba();
            bombita.Mover();
            System.Threading.Thread.Sleep(1000);//Pasa 1 segundo
            unJuego.AvanzarElTiempo();
            bombita.Movimiento.CambiarAAbajo();
            bombita.Mover();
            bombita.Movimiento.CambiarAIzquierda();
            bombita.Mover();
            bombita.Mover();
            bombita.Mover(); //come articulo BombaToleToe
            bombita.LanzarExplosivo();
            Assert.IsInstanceOf(typeof(BombaToleTole), this.unJuego.Ambiente.ObtenerCasilla(new Punto(7, 2)).Explosivo);
            Assert.IsFalse(bombita.Destruido());
        }

        [Test]
        public void LopezReggaeAladoPlantaUnaMolotovSeMueveFueraDeSuAlcanceYLuegoDeQueLaBombaExplotaVuelveYPlantaOtra()
        {
            Punto PosicionDePlantado = new Punto(0, 1);
            LosLopezReggaeAlado personaje = new LosLopezReggaeAlado(PosicionDePlantado);
            Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Transitar(personaje);
            personaje.LanzarExplosivo();
            personaje.Movimiento.CambiarADerecha();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            Juego.Instancia().AvanzarElTiempo();
            personaje.Movimiento.CambiarAIzquierda();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.Mover();
            personaje.LanzarExplosivo();
            Assert.IsInstanceOf(typeof(BombaMolotov), Juego.Instancia().Ambiente.ObtenerCasilla(PosicionDePlantado).Explosivo);
            Assert.IsFalse(personaje.Destruido());
        }

        [Test]
        public void LopezReggaeDisparaProyectilHaciaBloqueDeLadrilloAlImpactarDisminuyeSuResistenciaEnUnaUnidad()
        {
            Punto PosicionDePartida = new Punto(5, 0);
            LosLopezReggae personaje = new LosLopezReggae(PosicionDePartida);
            Casilla casillaConBloqueDeLadrillo = this.unJuego.Ambiente.ObtenerCasilla(new Punto(2, 0));
            int resistenciaBloque = casillaConBloqueDeLadrillo.Estado.UnidadesDeResistencia;
            //casillaAux.Estado = BloqueComun.CrearBloqueLadrillos();
            this.unJuego.AgregarEnemigo(personaje);
            personaje.Movimiento.CambiarAIzquierda();
            personaje.LanzarExplosivo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            this.unJuego.AvanzarElTiempo();
            Assert.AreEqual(resistenciaBloque - 1, casillaConBloqueDeLadrillo.Estado.UnidadesDeResistencia);
        }

        [Test]
        public void CecilioPlantaUnaBombaEnUnaCasillaTransitadaPorVariosEnemigosYEstosMuerenTodosPorElloNoHayNadieTransitandoDespues()
        {
            

            Punto puntoTransito = new Punto(5,0);
            Cecilio unCecilio = new Cecilio(puntoTransito);
            Cecilio otroCecilio = new Cecilio(puntoTransito);
            Cecilio Cecilio = new Cecilio(puntoTransito);
            LosLopezReggaeAlado unLRA = new LosLopezReggaeAlado(puntoTransito);
            LosLopezReggaeAlado otroLRA = new LosLopezReggaeAlado(puntoTransito);
            LosLopezReggaeAlado LRA = new LosLopezReggaeAlado(puntoTransito);
            Casilla casillaTransitada = Juego.Instancia().Ambiente.ObtenerCasilla(puntoTransito);

            casillaTransitada.Transitar(unCecilio);
            casillaTransitada.Transitar(Cecilio);
            casillaTransitada.Transitar(otroCecilio);
            casillaTransitada.Transitar(unLRA);
            casillaTransitada.Transitar(LRA);
            casillaTransitada.Transitar(otroLRA);

                       
            Assert.AreEqual(casillaTransitada.TransitandoEnCasilla.Count, 6);

            unCecilio.LanzarExplosivo();
            
            Assert.IsInstanceOf(typeof(BombaMolotov), casillaTransitada.Explosivo);
            System.Threading.Thread.Sleep(1000);//Pasa 1 segundo
            Juego.Instancia().AvanzarElTiempo();



            Assert.IsTrue(unCecilio.Destruido());
            Assert.IsTrue(LRA.Destruido());
            Assert.IsTrue(otroCecilio.Destruido());
            Assert.IsTrue(Cecilio.Destruido());
            Assert.IsTrue(unLRA.Destruido());
            Assert.IsTrue(otroLRA.Destruido());

            Assert.AreEqual(casillaTransitada.TransitandoEnCasilla.Count, 0);

        }
            







    }
}
