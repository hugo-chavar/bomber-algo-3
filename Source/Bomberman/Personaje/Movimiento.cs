using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombermanModel.Mapa;

namespace BombermanModel.Personaje
{
    public class Movimiento
    {
        private int direccion;
        private float velocidad;
        private const float VELOCIDADINICIAL = 1;

        public Movimiento()
        {
            //la dirección inicial es random
            Random direccionRandom = new Random();
            //direccion puede ser 2,4,6,8 y obienen su significado de la flecha del numPad en el teclado
            this.direccion = 2 * (direccionRandom.Next(4) + 1);
            this.Velocidad = VELOCIDADINICIAL;
        }

        public float Velocidad
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
            this.direccion = Tablero.IZQUIERDA;
        }

        public void CambiarADerecha()
        {
            this.direccion = Tablero.DERECHA;
        }

        public void CambiarAArriba()
        {
            this.direccion = Tablero.ARRIBA;
        }

        public void CambiarAAbajo()
        {
            this.direccion = Tablero.ABAJO;
        }
    }
}
