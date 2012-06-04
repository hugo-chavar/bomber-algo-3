using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Mapa.Casilla;
using Bomberman.Excepciones;

namespace Bomberman.Mapa
{
    public class Mapa : ITransitable
    {
        //private Casilla.Casilla[,] tablero;
        private Dictionary<Punto, Casilla.Casilla> tablero;
        private int dimensionHorizontal;
        private int dimensionVertical;

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
            //this.Tablero = (new Casilla.Casilla[this.dimensionHorizontal, this.dimensionVertical]);
            this.tablero = new Dictionary<Punto, Casilla.Casilla>();

        }

        public void agregarCasilla(Casilla.Casilla unaCasilla)
        {
            if (unaCasilla != null )
            {
                if (unaCasilla.Posicion != null)
                {
                    this.tablero.Add(unaCasilla.Posicion, unaCasilla);
                }
                else
                {
                    throw new PosicionNulaException();
                }
                
            }
            else
            {
                throw new NoExisteCasillaException();
            }
            
        }

        public bool existeCasillaEnPosicion(Punto pos)
        {
            if (pos == null)
                throw new PosicionNulaException();
            return this.tablero.ContainsKey(pos);
        }

        public Casilla.Casilla obtenerCasilla(Punto pos)
        {

            Casilla.Casilla unaCasilla;
            if (this.existeCasillaEnPosicion(pos))
            {
                unaCasilla = this.tablero[pos];
            }
            else
            {
                throw new NoExisteCasillaException();
            }
            return unaCasilla;

        }
    }
}
