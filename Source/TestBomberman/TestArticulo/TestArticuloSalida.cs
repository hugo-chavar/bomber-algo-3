using NUnit;
using NUnit.Framework;
using BombermanModel;
using BombermanModel.Mapa;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Excepciones;
using BombermanModel.Personaje;
using BombermanModel.Arma;
using BombermanModel.Articulo;
using BombermanModel.Juego;
using System.Collections.Generic;

namespace TestBombermanModel.TestSalida
{
    [TestFixture]
    class TestSalida
    {
        private const int ANCHOMAPA = 5;
        private const int ALTOMAPA = 5;
        private Mapa unMapa;

 
   

        [SetUp]
        public void TestSetup()
        {
            //creo un mapa 5x5 con esta distribucion (P = Pasillo, * = BloqueAcero):
            //      P P P P P
            //      P * P * P
            //      P P P P P
            //      P * P * P
            //      P P P P P
            Juego.Reiniciar();
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
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(unaPosicion);//.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    {
                        //uno de los dos es par
                        unaCasilla = FabricaDeCasillas.FabricarPasillo(unaPosicion);
                    }
                    this.unMapa.AgregarCasilla(unaCasilla);
                }
            Juego.Instancia().Ambiente = unMapa;
            Juego.Instancia().EnemigosVivos = new List<Personaje>();
        }

        [Test]
        public void PuedoAgregarSalidaEnUnBloqueLadrilloPuesEstaPermitido()
        {
            Punto unaPosicion = new Punto(0, 0);
            Casilla unaCasilla = new Casilla(unaPosicion);
            Mapa otroMapa = Juego.Instancia().Ambiente;

            Punto pUnaSalida = new Punto (1,1);

            Casilla unaCasillaDeSalida = unMapa.ObtenerCasilla(pUnaSalida);


            
            // agrego articulo
            Salida salida = new Salida();
            unaCasillaDeSalida.agregarSalida(salida);

            
            Assert.IsInstanceOf(typeof(Salida), unaCasillaDeSalida.ArticuloContenido);


        }

        [Test]
        public void NoPuedoAgregarSalidaEnUnPasilloPuesNoEstaPermitido()
        {
            // agrego articulo

            Punto pUnaSalida = new Punto(1, 0);


            Casilla unaCasillaDeSalida = unMapa.ObtenerCasilla(pUnaSalida);

            Salida salida = new Salida();
            unaCasillaDeSalida.agregarSalida(salida);



            Assert.IsNotInstanceOf(typeof(Salida), unaCasillaDeSalida.ArticuloContenido);


        }


        [Test]
        public void CuandoEnOtroMapaAgrego2PersonajesEliminoAlUnicoEnemigoYSeActivaLaSalida()
        {
            // agrego articulo
            Mapa otroMapa = Juego.Instancia().Ambiente;
            Punto pUnaSalida = new Punto(3, 3);
            Punto pUnCecilio = new Punto(2, 1);
            Punto pUnaBombaMolotov = new Punto(2, 0);
            Punto pBombita = new Punto(4, 4);
            
            Casilla unaCasillaDeSalida = Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida);
            unaCasillaDeSalida.agregarSalida(Juego.Instancia().Salida);

            Cecilio unCecil = new Cecilio(pUnCecilio);
            Bombita unBombita = new Bombita(pBombita);
            BombaMolotov unaBomba = new BombaMolotov(pUnaBombaMolotov, 0);

            otroMapa.AgregarPersonaje(unBombita);
            otroMapa.AgregarPersonaje(unCecil);
            Juego.Instancia().EnemigosVivos.Add(unCecil);

            Casilla casillaBomba = otroMapa.ObtenerCasilla(pUnaBombaMolotov);
            Juego.Instancia().AlojarExplosivo(unaBomba);

            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();

            Assert.AreEqual(0, Juego.Instancia().CantidadEnemigosVivos());
            Assert.IsTrue(Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).ArticuloContenido.EstaActivo );

        }

        [Test]
        public void CuandoEnOtroMapaAgrego3PersonajesEliminoSoloUnEnemigoYNoSeActivaLaSalida()
        {
            // agrego articulo
            Mapa otroMapa = Juego.Instancia().Ambiente;
            Punto pUnaSalida = new Punto(3, 3);
            Punto pUnCecilio = new Punto(2, 1);
            Punto pUnaBombaMolotov = new Punto(2, 0);
            Punto pBombita = new Punto(4, 4);
            Punto pLopez = new Punto(4, 3);

            Casilla unaCasillaDeSalida = Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida);
            Salida salida = new Salida();
            unaCasillaDeSalida.agregarSalida(salida);

            Assert.IsInstanceOf(typeof(Salida), Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).ArticuloContenido);

            Cecilio unCecil = new Cecilio(pUnCecilio);
            Bombita unBombita = new Bombita(pBombita);
            BombaMolotov unaBomba = new BombaMolotov(pUnaBombaMolotov, 0);
            LosLopezReggae lopez = new LosLopezReggae(pLopez);



            otroMapa.AgregarPersonaje(unBombita);
            otroMapa.AgregarPersonaje(unCecil);
            Juego.Instancia().EnemigosVivos.Add(unCecil);
            otroMapa.AgregarPersonaje(lopez);
            Juego.Instancia().EnemigosVivos.Add(lopez);

            Juego.Instancia().AlojarExplosivo(unaBomba);

            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();
            Juego.Instancia().AvanzarElTiempo();


            Assert.AreEqual(Juego.Instancia().CantidadEnemigosVivos(),1);
            Assert.IsFalse(Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).ArticuloContenido.EstaActivo);

        }
            










 }
}
