using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman;

namespace Bomberman.Personaje
{
    public class ManejadorDeMovimiento
    {
        private Personaje personajeInterno;
        private Mapa.Mapa unMapa;

        // Definir ENUM: 1: arriba, 2 : abajo, 3 : Derecha, 4 : Izquierda //

        public ManejadorDeMovimiento(Personaje personaje, Mapa.Mapa unMapa)
        {
            this.personajeInterno = personaje;
            this.unMapa = unMapa;

        }

        public Punto MoverPersonaje(int MOV){
            Punto posicionDestino = this.ConseguirDestino(personajeInterno.Posicion, MOV);
                if (!(posicionDestino.EsPuntoValido()))
                {
                  return personajeInterno.Posicion;
                }
                Bomberman.Mapa.Casilla.Casilla casilleroDestino = unMapa.ObtenerCasilla(posicionDestino);
                Bomberman.Mapa.Casilla.Casilla casilleroOrigen = unMapa.ObtenerCasilla(personajeInterno.Posicion);
             if (casilleroDestino.PermiteTransitarUn(personajeInterno))
            {
                // casilleroOrigen.Destransitar(personajeInterno) falta Destransitar //
                personajeInterno.Posicion = posicionDestino;
                casilleroDestino.Transitar(personajeInterno);
                return posicionDestino;
            }
             return personajeInterno.Posicion;
            }
        

        public Punto ConseguirDestino(Punto Posicion, int MOV)
        {
            switch (MOV)
            {
                case 0:                  // Arriba //
                    {
                        Posicion.PosicionSuperior(1);
                        break;
                    }
                case 1:             // Abajo //
                    {
                        Posicion.PosicionSuperior(-1);
                        break;
                    }
                case 2:             // Derecha //
                    {
                        Posicion.PosicionDerecha(1);
                        break;
                    }
                case 3:             // Izquierda //
                    {
                        Posicion.PosicionDerecha(-1);
                        break;
                    }
            }
            return Posicion;
        }













}
}


 
