using NUnit;
using NUnit.Framework;
using Bomberman;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;
using Bomberman.Excepciones;
using Bomberman.Personaje;
using Bomberman.Arma;
using Bomberman.Articulo;
using Bomberman.Juego;

namespace TestBomberman.TestSalida
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


        }



        [Test]
        public void PuedoAgregarSalidaEnUnBloqueLadrilloPuesEstaPermitido()
        {
            Mapa otroMapa = new Mapa(5,5);
            Punto unaPosicion = new Punto(0, 0);
            Casilla unaCasilla = new Casilla(unaPosicion);

            // Inicializo otra vez el mapa porque falla el SetUp.
            int i, j;
            for (i = 0; i < ANCHOMAPA; i++)
                for (j = 0; j < ANCHOMAPA; j++)
                {
                    Punto otraPosicion = new Punto(i, j);
                    if ((i & 1) == 1 && (j & 1) == 1)
                    {
                        //ambos son numeros impares
                        unaCasilla = FabricaDeCasillas.FabricarCasillaConBloqueLadrillos(otraPosicion);//.FabricarCasillaConBloqueAcero(unaPosicion);
                    }
                    else
                    {
                        //uno de los dos es par
                        unaCasilla = FabricaDeCasillas.FabricarPasillo(otraPosicion);
                    }

                    otroMapa.AgregarCasilla(unaCasilla);
                }

            Juego.Instancia().Ambiente = otroMapa;
            Punto pUnaSalida = new Punto (1,1);

            Casilla unaCasillaDeSalida = unMapa.ObtenerCasilla(pUnaSalida);


            
            // agrego articulo
            unaCasillaDeSalida.agregarSalida();

            
            Assert.IsInstanceOf(typeof(Salida), unaCasillaDeSalida.ArticuloContenido);


        }

        [Test]
        public void NoPuedoAgregarSalidaEnUnPasilloPuesNoEstaPermitido2()
        {
            // agrego articulo

            Punto pUnaSalida = new Punto(1, 0);


            Casilla unaCasillaDeSalida = unMapa.ObtenerCasilla(pUnaSalida);

            unaCasillaDeSalida.agregarSalida();



            Assert.IsNotInstanceOf(typeof(Salida), unaCasillaDeSalida.ArticuloContenido);


        }


        [Test]
        public void CuandoEnOtroMapaAgrego2PersonajesEliminoAlUnicoEnemigoYSeActivaLaSalida()
        {
            // agrego articulo
            Punto unaPosicion;
            Casilla unaCasilla;
            Mapa otroMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);

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

                    otroMapa.AgregarCasilla(unaCasilla);
                }

            Juego.Instancia().Ambiente = otroMapa;

            Punto pUnaSalida = new Punto(3, 3);
            Punto pUnCecilio = new Punto(2, 1);
            Punto pUnaBombaMolotov = new Punto(2, 0);
            Punto pBombita = new Punto(4, 4);
            Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).agregarSalida();
            Assert.IsInstanceOf(typeof(Salida), Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).ArticuloContenido);

            Cecilio unCecil = new Cecilio(pUnCecilio);
            Bombita unBombita = new Bombita(pBombita);
            BombaMolotov unaBomba = new BombaMolotov(pUnaBombaMolotov, 0);

            otroMapa.AgregarPersonaje(unBombita);
            otroMapa.AgregarPersonaje(unCecil);

            Casilla casillaBomba = otroMapa.ObtenerCasilla(pUnaBombaMolotov);
            casillaBomba.PlantarExplosivo(unaBomba);

            Juego.Instancia().AvanzarElTiempo();


            Assert.IsTrue(Juego.Instancia().EnemigosVivos() == 0);
            Assert.IsTrue(Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).ArticuloContenido.EstaActivo );

        }

        [Test]
        public void CuandoEnOtroMapaAgrego3PersonajesEliminoSoloUnEnemigoYNoSeActivaLaSalida()
        {
            // agrego articulo
            Punto unaPosicion;
            Casilla unaCasilla;
            Mapa otroMapa = new Mapa(ANCHOMAPA, ANCHOMAPA);

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

                    otroMapa.AgregarCasilla(unaCasilla);
                }

            Juego.Instancia().Ambiente = otroMapa;

            Punto pUnaSalida = new Punto(3, 3);
            Punto pUnCecilio = new Punto(2, 1);
            Punto pUnaBombaMolotov = new Punto(2, 0);
            Punto pBombita = new Punto(4, 4);
            Punto pLopez = new Punto(4, 3);

            Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).agregarSalida();
            Assert.IsInstanceOf(typeof(Salida), Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).ArticuloContenido);

            Cecilio unCecil = new Cecilio(pUnCecilio);
            Bombita unBombita = new Bombita(pBombita);
            BombaMolotov unaBomba = new BombaMolotov(pUnaBombaMolotov, 0);
            LosLopezReggae lopez = new LosLopezReggae(pLopez);



            otroMapa.AgregarPersonaje(unBombita);
            otroMapa.AgregarPersonaje(unCecil);
            otroMapa.AgregarPersonaje(lopez);


            Casilla casillaBomba = otroMapa.ObtenerCasilla(pUnaBombaMolotov);
            casillaBomba.PlantarExplosivo(unaBomba);

            Juego.Instancia().AvanzarElTiempo();


            Assert.IsTrue(Juego.Instancia().EnemigosVivos() == 1);
            Assert.IsFalse(Juego.Instancia().Ambiente.ObtenerCasilla(pUnaSalida).ArticuloContenido.EstaActivo);

        }
            










 }
}
