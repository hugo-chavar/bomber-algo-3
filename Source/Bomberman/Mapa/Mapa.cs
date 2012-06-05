using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Arma;
using Bomberman.Mapa.Casilla;
using Bomberman.Excepciones;

namespace Bomberman.Mapa
{
    public class Mapa : ITransitable
    {
        private Dictionary<Punto, Casilla.Casilla> tablero;
        private int dimensionHorizontal;
        private int dimensionVertical;
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;

        public Dictionary<Punto, Casilla.Casilla> Tablero
        {
            get { return this.tablero; }
            set { this.tablero = value; }
        }

        public int DimensionHorizontal
        {
            get { return this.dimensionHorizontal; }
        }

        public int DimensionVertical
        {
            get { return this.dimensionVertical; }
        }

        public Mapa(int tamanioHorizontal, int tamanioVertical)
        {
            this.dimensionHorizontal = tamanioHorizontal;
            this.dimensionVertical = tamanioVertical;
            this.tablero = new Dictionary<Punto, Casilla.Casilla>();

        }

        public void AgregarCasilla(Casilla.Casilla unaCasilla)
        {
            if (unaCasilla == null )
            {
                throw new NoExisteCasillaException();
            }
            if (unaCasilla.Posicion == null)
            {
                throw new PosicionNulaException();
            }
            if (!PosicionDentroRango(unaCasilla.Posicion))
            {
                throw new PuntoFueraDeRangoEnMapaException();
            }
            try
            {
                this.tablero.Add(unaCasilla.Posicion, unaCasilla);
            }
            catch (System.ArgumentException)
            {
                throw new CasillaYaIngresadaException();
            }
            
        }

        private bool PosicionDentroRango(Punto punto)
        {
            return (punto.X < this.DimensionHorizontal && punto.Y < this.DimensionVertical);
        }

        public bool ExisteCasillaEnPosicion(Punto pos)
        {
            if (pos == null)
                throw new PosicionNulaException();
            return this.tablero.ContainsKey(pos);
        }

        public Casilla.Casilla ObtenerCasilla(Punto pos)
        {

            Casilla.Casilla unaCasilla;
            if (this.ExisteCasillaEnPosicion(pos))
            {
                unaCasilla = this.tablero[pos];
            }
            else
            {
                throw new NoExisteCasillaException();
            }
            return unaCasilla;

        }

        public bool PermitidoMoverHaciaArribaA(Personaje.IMovible movil)
        {
            Punto posicionSuperior = movil.Posicion.PosicionSuperior();
            if (!this.ExisteCasillaEnPosicion(posicionSuperior))
            {
                return false;
            }
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionSuperior);
            return unaCasilla.PermiteTransitarUn(movil);
        }

        public bool PermitidoMoverHaciaAbajoA(Personaje.IMovible movil)
        {
            Punto posicionInferior = movil.Posicion.PosicionInferior();
            if (!this.ExisteCasillaEnPosicion(posicionInferior))
            {
                return false;
            }
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionInferior);
            return unaCasilla.PermiteTransitarUn(movil);
        }

        public bool PermitidoMoverHaciaIzquierdaA(Personaje.IMovible movil)
        {
            Punto posicionIzquierda = movil.Posicion.PosicionIzquierda();
            if (!this.ExisteCasillaEnPosicion(posicionIzquierda))
            {
                return false;
            }
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionIzquierda);
            return unaCasilla.PermiteTransitarUn(movil);
        }

        public bool PermitidoMoverHaciaDerechaA(Personaje.IMovible movil)
        {
            Punto posicionDerecha = movil.Posicion.PosicionDerecha();
            if (!this.ExisteCasillaEnPosicion(posicionDerecha))
            {
                return false;
            }
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionDerecha);
            return unaCasilla.PermiteTransitarUn(movil);
        }

        public void Mover(Personaje.IMovible movil)
        {
            switch (movil.Movimiento.Direccion)
            {
                case ARRIBA:
                    {
                        if (PermitidoMoverHaciaArribaA(movil))
                            MoverHaciaArribaA(movil);
                        break;
                    }
                case ABAJO:
                    {
                        if (PermitidoMoverHaciaAbajoA(movil))
                            MoverHaciaAbajoA(movil);
                        break;

                    }
                case IZQUIERDA:
                    {
                        if (PermitidoMoverHaciaIzquierdaA(movil))
                            MoverHaciaIzquierdaA(movil);
                        break;
                    }
                case DERECHA:
                    {
                        if (PermitidoMoverHaciaDerechaA(movil))
                            MoverHaciaDerechaA(movil);
                        break;

                    }
            }
        }

        private void MoverHaciaDerechaA(Personaje.IMovible movil)
        {
            Punto posicionDerecha = movil.Posicion.PosicionDerecha();
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionDerecha);
            unaCasilla.Transitar(movil);
            Casilla.Casilla otraCasilla;
            otraCasilla = this.ObtenerCasilla(movil.Posicion);
            otraCasilla.Dejar(movil);
        }

        private void MoverHaciaIzquierdaA(Personaje.IMovible movil)
        {
            Punto posicionIzquierda = movil.Posicion.PosicionIzquierda();
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionIzquierda);
            unaCasilla.Transitar(movil);
            Casilla.Casilla otraCasilla;
            otraCasilla = this.ObtenerCasilla(movil.Posicion);
            otraCasilla.Dejar(movil);
        }

        private void MoverHaciaAbajoA(Personaje.IMovible movil)
        {
            Punto posicionAbajo = movil.Posicion.PosicionInferior();
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionAbajo);
            unaCasilla.Transitar(movil);
            Casilla.Casilla otraCasilla;
            otraCasilla = this.ObtenerCasilla(movil.Posicion);
            otraCasilla.Dejar(movil);
        }

        private void MoverHaciaArribaA(Personaje.IMovible movil)
        {
            Punto posicionSuperior = movil.Posicion.PosicionSuperior();
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionSuperior);
            unaCasilla.Transitar(movil);
            Casilla.Casilla otraCasilla;
            otraCasilla = this.ObtenerCasilla(movil.Posicion);
            otraCasilla.Dejar(movil);
        }
        
        //Por el momento atrapo solo la excepcion.Hay qu solucionarlo de otr Forma
        //Quien esta haciendo esto? soy Hugo, modifico el casteo fiero y dejo comentado las lineas que saco
        public void ManejarExplosion(Explosivo explosivo)
        {
            //ArrayList puntosAfectados = CalcularCasillerosExplotados(explosivo);
            List<Punto> puntosAfectados = CalcularCasillerosExplotados(explosivo);
            for (int i = 0; i < (puntosAfectados.Count); i++)
            {
                try
                {
                    //Casilla.Casilla casillaAux = this.ObtenerCasilla((Punto)puntosAfectados[i]);
                    Casilla.Casilla casillaAux = this.ObtenerCasilla(puntosAfectados[i]);
                    explosivo.Daniar(casillaAux.Estado);
                    for (int j = 0; j < casillaAux.TransitandoEnCasilla.Count; j++)
                        explosivo.Daniar(casillaAux.TransitandoEnCasilla[j]);
                }
                catch (NoExisteCasillaException)
                {
                    //simplemente se ignora donde no hay casillas
                }
            }
        }

        //Volver a mirar Este Metodo. Soluciono asi Para ver si Funcionan Tests
        //Este metodo podria tener codigo que explica lo que hace porque no se entiende
        private List<Punto> CalcularCasillerosExplotados(Explosivo explosivo)
        {
            List<Punto> listaDevolucion = new List<Punto>();
            Punto unPuntoAux = new Punto(explosivo.Posicion.X - explosivo.OndaExpansiva, explosivo.Posicion.Y);

            for (int i = 0; i <= 2*explosivo.OndaExpansiva; i++)
            {
                unPuntoAux.PosicionDerecha(1);
                listaDevolucion.Add(unPuntoAux);
            }

            unPuntoAux.X = explosivo.Posicion.X;
            unPuntoAux.Y = explosivo.Posicion.Y - explosivo.OndaExpansiva;
            for (int i = 0; i <= 2*explosivo.OndaExpansiva; i++)
            {
                unPuntoAux.PosicionSuperior(1);
                if (unPuntoAux != explosivo.Posicion)
                    listaDevolucion.Add(unPuntoAux);
            }

            return listaDevolucion;
        }
    }
}
