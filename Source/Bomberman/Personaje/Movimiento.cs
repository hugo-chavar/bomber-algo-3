using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Personaje
{
    public class Movimiento
    {
        private int direccion;
        private int velocidad;
        private const int VELOCIDADINICIAL = 1;

        public Movimiento()
        {
            //la dirección inicial es random
            Random direccionRandom = new Random();
            //direccion puede ser 2,4,6,8 y obienen su significado de la flecha del numPad en el teclado
            this.direccion = 2 * (direccionRandom.Next(4) + 1);//Mapa.Mapa.IZQUIERDA; 
        }

        public int Velocidad
        {
            set { this.velocidad = value; }
            get { return this.velocidad; }
        }

        public int Direccion
        {
            set { this.direccion = value; }
            get { return this.direccion; }
        }

        public void MultiplicarVelocidadPor(int num)
        {
            this.velocidad = num * (this.velocidad);
        }
        
        public void CambiarAIzquierda()
        {
            this.direccion = Mapa.Mapa.IZQUIERDA;
        }

        public void CambiarADerecha()
        {
            this.direccion = Mapa.Mapa.DERECHA;
        }

        public void CambiarAArriba()
        {
            this.direccion = Mapa.Mapa.ARRIBA;
        }

        public void CambiarAAbajo()
        {
            this.direccion = Mapa.Mapa.ABAJO;
        }

    }
}
