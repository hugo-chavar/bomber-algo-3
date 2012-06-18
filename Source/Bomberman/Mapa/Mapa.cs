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
        public const int CANTIDADJUGADORES = 1;
        private bool nivelTerminado;
        private bool nivelGanado;
        private Punto posicionSalida;

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


        public bool NivelTerminado
        {
            get { return this.nivelTerminado; }
            set { this.nivelTerminado = value; }
        }

        public bool NivelGanado
        {
            get { return this.nivelGanado; }
            set { this.nivelGanado = value; }
        }

        public Punto PosicionSalida
        {
            get { return this.posicionSalida; }
            set { this.posicionSalida = value; }
        }

        public Mapa(int tamanioHorizontal, int tamanioVertical)
        {
            this.dimensionHorizontal = tamanioHorizontal;
            this.dimensionVertical = tamanioVertical;
            this.tablero = new Dictionary<Punto, Casilla.Casilla>();
            this.NivelGanado = false;
            this.NivelTerminado = false;
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
            if (pos == null)
                throw new PosicionNulaException();
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
                            {
                                Casilla.Casilla unaCasilla = ObtenerCasilla(movil.Posicion);
                                unaCasilla.Dejar(movil);
                                MoverHaciaArribaA(movil); 
                            }
                        break;
                    }
                case ABAJO:
                    {
                        if (PermitidoMoverHaciaAbajoA(movil))
                            {
                                Casilla.Casilla unaCasilla = ObtenerCasilla(movil.Posicion);
                                unaCasilla.Dejar(movil);
                                MoverHaciaAbajoA(movil);
                            }
                        break;

                    }
                case IZQUIERDA:
                    {
                        if (PermitidoMoverHaciaIzquierdaA(movil))
                        {
                            Casilla.Casilla unaCasilla = ObtenerCasilla(movil.Posicion);
                            unaCasilla.Dejar(movil);
                            MoverHaciaIzquierdaA(movil);
                        } 
                        break;
                    }
                case DERECHA:
                    {
                        if (PermitidoMoverHaciaDerechaA(movil))
                        {
                            Casilla.Casilla unaCasilla = ObtenerCasilla(movil.Posicion);
                            unaCasilla.Dejar(movil);
                            MoverHaciaDerechaA(movil);
                        } 
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
        }

        private void MoverHaciaIzquierdaA(Personaje.IMovible movil)
        {
            Punto posicionIzquierda = movil.Posicion.PosicionIzquierda();
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionIzquierda);
            movil.Posicion = posicionIzquierda;
            unaCasilla.Transitar(movil);
        }

        private void MoverHaciaAbajoA(Personaje.IMovible movil)
        {
            Punto posicionAbajo = movil.Posicion.PosicionInferior();
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionAbajo);
            movil.Posicion = posicionAbajo;
            unaCasilla.Transitar(movil);
        }

        private void MoverHaciaArribaA(Personaje.IMovible movil)
        {
            Punto posicionSuperior = movil.Posicion.PosicionSuperior();
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionSuperior);
            movil.Posicion = posicionSuperior;
            unaCasilla.Transitar(movil);
        }

        public void ManejarExplosion(Explosivo explosivo)
        {
            List<Punto> puntosAfectados = CalcularCasillerosExplotados(explosivo);
            this.ObtenerCasilla(explosivo.Posicion).QuitarExplosivo();

            for (int i = 0; i < (puntosAfectados.Count); i++)
            {
                Casilla.Casilla casillaAux = this.ObtenerCasilla(puntosAfectados[i]);
                explosivo.Daniar(casillaAux.Estado);
                if (casillaAux.Estado.Destruido())
                    casillaAux.Estado = new Pasillo();
                for (int j = 0; j < casillaAux.TransitandoEnCasilla.Count; j++)
                {
                    if (casillaAux.TransitandoEnCasilla[j].EsDaniable())
                        explosivo.Daniar((IDaniable)casillaAux.TransitandoEnCasilla[j]);
                }

                // El proximo while es una solucion importada de internet para resolver problema de indexado en la lista
                int iterador = casillaAux.TransitandoEnCasilla.Count;
                while (--iterador >= 0)
                {
                    if ((casillaAux.TransitandoEnCasilla[iterador].EsDaniable()))
                    {
                        if (((IDaniable)casillaAux.TransitandoEnCasilla[iterador]).Destruido())
                            casillaAux.TransitandoEnCasilla.RemoveAt(iterador);
                    }
                }

            }
        }
 
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
            Punto unPuntoAux = punto.PosicionIzquierda(); 
            while (this.PosicionDentroRango(unPuntoAux) && i <= expansion)
            {
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = unPuntoAux.PosicionIzquierda();
            }
        }

        private void AgregarCasillerosADerecha(List<Punto> Lista, int expansion, Punto punto)
        {
            int i = 1;
            Punto unPuntoAux = punto.PosicionDerecha();
            while ((this.PosicionDentroRango(unPuntoAux)) && (i <= expansion))
            {
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = unPuntoAux.PosicionDerecha();
            }
        }

        private void AgregarCasillerosArriba(List<Punto> Lista, int expansion, Punto punto)
        {
            int i = 1;
            Punto unPuntoAux = punto.PosicionSuperior();
            while ((this.PosicionDentroRango(unPuntoAux)) && (i <= expansion))
            {
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = unPuntoAux.PosicionSuperior();
            }
        }

        private void AgregarCasillerosAbajo(List<Punto> Lista, int expansion, Punto punto)
        {
            int i = 1;
            Punto unPuntoAux = punto.PosicionInferior();
            while (this.PosicionDentroRango(unPuntoAux) && (i <= expansion)) 
            {
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = unPuntoAux.PosicionInferior();
            }
        }
        
        public void FinalizarNivel()
        {
            this.NivelTerminado = true;
            this.NivelGanado = true;
        }

        public bool PermitidoLanzarExplosivoAPos(Punto pos)
        {
            return (ExisteCasillaEnPosicion(pos) && ObtenerCasilla(pos).PermiteExplosivos() && !ObtenerCasilla(pos).TieneUnExplosivo());
        }

        public void ResolverColisionesCon(Personaje.IMovible movil)
        {
            if (movil.ImpactaEnObstaculos() && ExisteCasillaEnPosicion(movil.Posicion) && !ObtenerCasilla(movil.Posicion).PermiteExplosivos())
            {
                movil.Colisionar();
            }
        }
    }
}
