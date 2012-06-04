using System;
using System.Collections;
using System.Linq;
using System.Text;
using Bomberman.Mapa;
using Bomberman.Mapa.Casilla;

namespace Bomberman.Arma
{
    class ManejadorExplosiones
    {
        private Explosivo explosivo;

        public ManejadorExplosiones(Explosivo explotable)
        {
            this.explosivo = explotable;
        }

        //Ver de mejorar logica subdiviendo en metodos
        private ArrayList CalcularCasillerosExplotados()
        {
            ArrayList listaDevolucion = new ArrayList();
            Punto unPuntoAux = new Punto(1, this.explosivo.Posicion.Y);

            for (int i = -(this.explosivo.OndaExpansiva); i <= this.explosivo.OndaExpansiva; i++)
            {
                unPuntoAux.PosicionDerecha(1);
                listaDevolucion.Add(unPuntoAux);
            }

            unPuntoAux.X = this.explosivo.Posicion.X;
            for (int i = -(this.explosivo.OndaExpansiva); i <= this.explosivo.OndaExpansiva; i++)
            {
                unPuntoAux.PosicionSuperior(1);
                if (unPuntoAux != this.explosivo.Posicion)
                listaDevolucion.Add(unPuntoAux);
            }

            return listaDevolucion;
        }

        public void ManejarExplosion()
        {
            ArrayList puntosAfectados = this.CalcularCasillerosExplotados();
            for (int i = 1; i < (puntosAfectados.Count) + 1; i++)
            {
                //Casilla casillero= Juego.Instancia.Mapa.Posicion(puntosAfectados[i]);
                //this.explosivo.daniar(casillero);

            }
        
        }



    }
}
