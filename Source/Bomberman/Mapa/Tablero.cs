using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Arma;
using BombermanModel.Mapa.Casilla;
using BombermanModel.Excepciones;

namespace BombermanModel.Mapa
{
    public class Tablero
    {
        private Dictionary<Punto, Casilla.Casilla> tablero = new Dictionary<Punto, Casilla.Casilla>();
        private int dimensionHorizontal;
        private int dimensionVertical;
        public const int ARRIBA = 8;
        public const int ABAJO = 2;
        public const int IZQUIERDA = 4;
        public const int DERECHA = 6;
        private bool nivelTerminado = false;
        private bool nivelGanado = false;
        private Punto posicionSalida;
        private Punto posicionInicial;
        private string stageName;
        private int nroNivel;

        public int DimensionHorizontal
        {
            get { return this.dimensionHorizontal; }
            set { this.dimensionHorizontal = value; }
        }

        public int NroNivel
        {
            get { return this.nroNivel; }
            set { this.nroNivel = value; }
        }

        public string StageName
        {
            get { return this.stageName; }
            set { this.stageName = value; }
        }

        public int DimensionVertical
        {
            get { return this.dimensionVertical; }
            set { this.dimensionVertical = value; }
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

        public Punto PosicionInicial
        {
            get { return this.posicionInicial; }
            set { this.posicionInicial = value; }
        }

        public Tablero(int tamanioHorizontal, int tamanioVertical)
        {
            this.dimensionHorizontal = tamanioHorizontal;
            this.dimensionVertical = tamanioVertical;
        }

        public Tablero()
        {
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

        public bool PermitidoAvanzar(Personaje.IMovible movil)
        {
            Punto posicionDestino = movil.PosicionDestino();
            if (!this.ExisteCasillaEnPosicion(posicionDestino))
            {
                return false;
            }
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionDestino);
            return unaCasilla.PermiteTransitarUn(movil);
        }

        public void Avanzar(Personaje.IMovible movil)
        {
            Punto posicionDestino = movil.PosicionDestino();
            Punto posicionAnterior = movil.Posicion; 
            Casilla.Casilla unaCasilla;
            unaCasilla = this.ObtenerCasilla(posicionDestino);
            movil.Posicion = posicionDestino;
            unaCasilla.Transitar(movil);
            unaCasilla = ObtenerCasilla(posicionAnterior);
            unaCasilla.Dejar(movil);
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
 
      private List<Punto> CalcularCasillerosExplotados(Explosivo explosivo)
        {
            List<Punto> listaDevolucion = new List<Punto>();
            listaDevolucion.Add(explosivo.Posicion);
            this.AgregarCasilleros(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion, DERECHA);
            this.AgregarCasilleros(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion, IZQUIERDA);
            this.AgregarCasilleros(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion, ARRIBA);
            this.AgregarCasilleros(listaDevolucion, explosivo.OndaExpansiva, explosivo.Posicion, ABAJO);
            return listaDevolucion;
        }

      private void AgregarCasilleros(List<Punto> Lista, int expansion, Punto punto, int direccion)
      {
          int i = 1;
          Punto unPuntoAux = punto.PosicionHaciaUnaDireccion(direccion);
          Casilla.Casilla unaCasillaAnteriorAux = new Casilla.Casilla(unPuntoAux);
          unaCasillaAnteriorAux = FabricaDeCasillas.FabricarPasillo(unPuntoAux);
          while (this.PosicionDentroRango(unPuntoAux) && i <= expansion && unaCasillaAnteriorAux.Estado.GetType() == typeof(Pasillo))
          {
              unaCasillaAnteriorAux = Juego.Juego.Instancia().Ambiente.ObtenerCasilla(unPuntoAux);
              Lista.Add(unPuntoAux);
              i++;
              unPuntoAux = unPuntoAux.PosicionHaciaUnaDireccion(direccion);
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
