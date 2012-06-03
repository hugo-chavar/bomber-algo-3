using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Personaje
{
    class ManejadorDeMovimiento
    {
        private Personaje personajeInterno;

        // Definir ENUM: 1: arriba, 2 : abajo, 3 : Derecha, 4 : Izquierda //

        public ManejadorDeMovimiento(Personaje personaje)
        {
            this.personajeInterno = personaje;
        }

        public Punto MoverPersonaje(int MOV){
            Punto posicionDestino = this.ConseguirDestino(personajeInterno.Posicion, MOV);
                if (!(posicionDestino.EsPuntoValido()))
                {
                  return personajeInterno.Posicion;
                }
            Bomberman.Mapa.Casilla.Casilla casilleroDestino = Juego.Juego.Instancia().Ambiente.obtenerCasilla(posicionDestino);
            Bomberman.Mapa.Casilla.Casilla casilleroOrigen =  Juego.Juego.Instancia().Ambiente.obtenerCasilla(personajeInterno.Posicion);
             if (casilleroDestino.PermiteTransitarUn(personajeInterno))
            {
                // casilleroOrigen.Destransitar(personajeInterno) falta Destransitar //
                personajeInterno.Posicion = posicionDestino;
                casilleroDestino.Transitar(personajeInterno);
            }
                return posicionDestino;
            }
        

        public Punto ConseguirDestino(Punto Posicion, int MOV)
        {
            switch (MOV)
            {
                case 0:                  // Arriba //
                    {
                        Posicion.AumentarYEn(1);
                        break;
                    }
                case 1:             // Abajo //
                    {
                        Posicion.AumentarYEn(-1);
                        break;
                    }
                case 2:             // Derecha //
                    {
                        Posicion.AumentarXEn(1);
                        break;
                    }
                case 3:             // Izquierda //
                    {
                        Posicion.AumentarXEn(-1);
                        break;
                    }
            }
            return Posicion;
        }













}
}


 
