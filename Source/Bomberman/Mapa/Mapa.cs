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

        public void AgregarPersonaje(Personaje.IMovible movil)
        {
            if (movil == null)
            {
                throw new NoPuedeAgregarMovilNuloException();
            }
            if (movil.Posicion == null)
            {
                throw new PosicionNulaException();
            }
            if (!PosicionDentroRango(movil.Posicion))
            {
                throw new PuntoFueraDeRangoEnMapaException();
            }
            if (!ExisteCasillaEnPosicion(movil.Posicion))
            {
                throw new NoExisteCasillaException();
            }

            Casilla.Casilla unaCasilla = ObtenerCasilla(movil.Posicion);
            unaCasilla.Transitar(movil);
        }

        public bool PosicionDentroRango(Punto punto)
        {
            return (punto.X < this.DimensionHorizontal && punto.Y < this.DimensionVertical && punto.X >= 0 && punto.Y >= 0);
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
            movil.Posicion = posicionDerecha;
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
            movil.Posicion = posicionIzquierda;
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
            movil.Posicion = posicionAbajo;
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
            movil.Posicion = posicionSuperior;
            unaCasilla.Transitar(movil);
            Casilla.Casilla otraCasilla;
            otraCasilla = this.ObtenerCasilla(movil.Posicion);
            otraCasilla.Dejar(movil);
        }
        
        //Por el momento atrapo solo la excepcion.Hay qu solucionarlo de otr Forma
        //Hugo dice:Andy esto es lo que vos decias que no va a lanzar la excepcion no?
        public void ManejarExplosion(Explosivo explosivo)
        {
            List<Punto> puntosAfectados = CalcularCasillerosExplotados(explosivo);
            for (int i = 0; i < (puntosAfectados.Count); i++)
            {
                try
                {
                    Casilla.Casilla casillaAux = this.ObtenerCasilla(puntosAfectados[i]);
                    explosivo.Daniar(casillaAux.Estado); 
                    if (casillaAux.Estado.Destruido())
                        casillaAux.Estado = new Pasillo();
                    for (int j = 0; j < casillaAux.TransitandoEnCasilla.Count; j++)
                        explosivo.Daniar(casillaAux.TransitandoEnCasilla[j]);
                }
                catch (NoExisteCasillaException)
                {
                    //simplemente se ignora donde no hay casillas
                }
            }
        }

            // Problema arreglado: solo falta refactorizar la repeticion de codigo 
      public List<Punto> CalcularCasillerosExplotados(Explosivo explosivo)
        {
            List<Punto> listaDevolucion = new List<Punto>();
            listaDevolucion.Add(explosivo.Posicion);
            this.AgregarCasillerosAbajo(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion);
            this.AgregarCasillerosArriba(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion);
            this.AgregarCasillerosAIzquierda(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion);
            this.AgregarCasillerosADerecha(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion);
            return listaDevolucion;
        }

        private void AgregarCasillerosAIzquierda(List<Punto> Lista,int expansion, Punto punto)
        { 
            int i=1;
            Punto unPuntoAux=new Punto(punto.X-1,punto.Y);
            while (unPuntoAux.EsPuntoValido() && i <= expansion) // Hugo dice: en lugar de unPuntoAux.EsPuntoValido() deberia ir this.PosicionDentroRango(unPuntoAux), ver explicacion abajo
            {
                
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = new Punto(punto.X - i, punto.Y);
                
            }
        }

        private void AgregarCasillerosADerecha(List<Punto> Lista, int expansion, Punto punto)
        {
            int i = 1;
            Punto unPuntoAux = new Punto(punto.X + 1, punto.Y);
            while ((this.PosicionDentroRango(unPuntoAux)) && (i <= expansion))
            {
               
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = new Punto(punto.X + i, punto.Y);
                
            }
        }

        private void AgregarCasillerosArriba(List<Punto> Lista, int expansion, Punto punto)
        {
            int i = 1;
            Punto unPuntoAux = new Punto(punto.X, punto.Y+1);
            while ((this.PosicionDentroRango(unPuntoAux)) && (i <= expansion))
            {
                
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = new Punto(punto.X, punto.Y + i);
            }
        }

        private void AgregarCasillerosAbajo(List<Punto> Lista, int expansion, Punto punto)
        {
            int i = 1;
            Punto unPuntoAux = new Punto(punto.X, punto.Y - 1);
            while ((unPuntoAux.EsPuntoValido()) && (i <= expansion)) // Hugo dice: en lugar de unPuntoAux.EsPuntoValido() deberia ir this.PosicionDentroRango(unPuntoAux)
            {                                                         // porque no esta chequeando los margenes derecho e izquierdo
                                                                       //además se deberia eliminar el metodo EsPuntoValido() de la clase punto porque no la usamos en ningun lado mas
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = new Punto(punto.X, punto.Y - i);

            }
        }


}
}
