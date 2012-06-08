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
        private List<IDependienteDelTiempo> esperaParaExplotar;
        private int cantidadPersonajesVivos;
        private bool nivelTerminado;
        private bool nivelGanado;
        private Punto posicionSalida;

        public Dictionary<Punto, Casilla.Casilla> Tablero
        {
            get { return this.tablero; }
            set { this.tablero = value; }
        }

        public int CantidadPersonajesVivos
        {
            get { return this.cantidadPersonajesVivos; }
            set { this.cantidadPersonajesVivos = value; }
        }

        public int DimensionHorizontal
        {
            get { return this.dimensionHorizontal; }
        }

        public List<IDependienteDelTiempo> EsperaParaExplotar
        {
            get { return esperaParaExplotar; }
        }

        public int DimensionVertical
        {
            get { return this.dimensionVertical; }
        }

        public int ObtenerCantidadPersonajes()
        {
            return CANTIDADJUGADORES;
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
            this.esperaParaExplotar = new List<IDependienteDelTiempo>();
            this.cantidadPersonajesVivos = 0;
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
            // CHEQUEAR QUE LA CASILLA SEA TRANSITABLE!
            //No hace falta chequear si es transitable, eso lo hace le personaje al moverse
            Casilla.Casilla unaCasilla = ObtenerCasilla(movil.Posicion);
            unaCasilla.Transitar(movil);
            (this.cantidadPersonajesVivos)++;

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
            try
            {
                this.ObtenerCasilla(explosivo.Posicion).QuitarExplosivo(explosivo);
                for (int i = 0; i < (puntosAfectados.Count); i++)
                {
                    Casilla.Casilla casillaAux = this.ObtenerCasilla(puntosAfectados[i]);
                    explosivo.Daniar(casillaAux.Estado); 
                    if (casillaAux.Estado.Destruido())
                        casillaAux.Estado = new Pasillo();
                    for (int j = 0; j < casillaAux.TransitandoEnCasilla.Count; j++)
                        explosivo.Daniar(casillaAux.TransitandoEnCasilla[j]);
                }
            }
            catch (NoExisteCasillaException)
                {
                    //simplemente se ignora donde no hay casillas
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
            Punto unPuntoAux=new Punto(punto.X-1,punto.Y);
            while (this.PosicionDentroRango(unPuntoAux) && i <= expansion)
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
            while (this.PosicionDentroRango(unPuntoAux) && (i <= expansion)) 
            {
                Lista.Add(unPuntoAux);
                i++;
                unPuntoAux = new Punto(punto.X, punto.Y - i);
            }
        }
        
        public void CuandoPasaElTiempo()
        {
            if (this.esperaParaExplotar.Count > 0)
            {
                int i = 0;

                for (i = 0; i < (esperaParaExplotar.Count); i++)
                {
                    esperaParaExplotar[i].CuandoPasaElTiempo();
                }
                for (i = 0; i < (esperaParaExplotar.Count); i++)
                {
                    if (((Explosivo)esperaParaExplotar[i]).EstaExplotado())
                        this.esperaParaExplotar.RemoveAt(i);
                }

           }
            if (ChequearCantidadPersonajesVivos())
            {
                ActivarSalida();
            }


        }

        public bool ChequearCantidadPersonajesVivos()
        {
            return (this.CantidadPersonajesVivos == CANTIDADJUGADORES);
        }

        public void ActivarSalida()
        {
            if (this.PosicionSalida != null)
            {
                Casilla.Casilla unaCasilla = new Casilla.Casilla(this.PosicionSalida);
                unaCasilla = this.ObtenerCasilla(this.PosicionSalida);
                unaCasilla.ArticuloContenido.Activar();
            }
        }

        public void DecrementarCantidadDePersonajesVivos()
        {
            (this.cantidadPersonajesVivos) = this.cantidadPersonajesVivos - 1;
        }

        public void FinalizarNivel()
        {
            this.NivelTerminado = true;
            this.NivelGanado = true;
        }

        public bool PermitidoLanzarExplosivoAPos(Punto pos)
        {
            return (ExisteCasillaEnPosicion(pos) && ObtenerCasilla(pos).PermiteExplosivos());
        }
    }
}
