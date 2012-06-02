using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class Proyectil:Explosivo{

        

        // Buscar ENUMERADOS: ARRIBA = 1; ABAJO = 0; DERECHA = 2; IZQUIERDA = 3 //

    
        private int alcance = 3 ; // establezco arbitrariamente un alcance de 3 casilleros (cuanto avanza el proytectil antes de explotar) y explota //
        private Punto posicionFinal;
        private int poderDeDestruccion;
        

         public Proyectil(int x, int y)

        {
            this.poderDeDestruccion = 5;
            this.ondaExpansiva = 3;
            Punto PosicionFinal = new Punto(0, 0);
            Punto PosicionInicial = new Punto(x, y);
            posicionFinal = PosicionFinal;
            posicion = PosicionInicial;

        }

    

        public override void daniar(IDaniable daniable)
        {
            //daniable.daniarConProyectil();
        }

        
        private Punto calcularZonaImpacto (int direccionPersonaje)
        {
            // no es del todo feliz esta resolucion pero al menos por ahora lo dejo //
            switch(direccionPersonaje)
            {
                case 0:
                    
                   posicionFinal.X = this.Posicion.X;
                   posicionFinal.Y = this.Posicion.Y - alcance;
                   break;
                case 1:
                    
                   posicionFinal.X = this.Posicion.X;
                   posicionFinal.Y = this.Posicion.Y + alcance;
                   break;
                case 3:
                    
                   posicionFinal.X = this.Posicion.X - alcance;
                   posicionFinal.Y = this.Posicion.Y;
                   break;       
                
                case 4:
                    
                   posicionFinal.X = this.Posicion.X + alcance;
                   posicionFinal.Y = this.Posicion.Y;

                    break;

        

             }
            return posicionFinal;
        }

        public void avanzarHacia(int direccion)
        {
            switch (direccion)
            {
                case 0:
                    posicion.Y = posicion.Y - 1;
                    break;
                case 1:
                    posicion.Y = posicion.Y + 1;
                    break;
                case 2:
                    posicion.X = posicion.X + 1;
                    break;
                case 3:
                    posicion.X = posicion.X - 1;
                    break;
            }
        }

        public int despegarProyectil(int direccionPersonaje)
        {
            Punto puntoFinal;
            puntoFinal = this.calcularZonaImpacto(direccionPersonaje);   //falta chequear que los valores X e Y finales no sean negativos, pero creo que otra clase en un nivel mas arriba deberia implementar eso //
            if (puntoFinal.X == Posicion.X)
            {
                while (posicion.Y != puntoFinal.Y)
                {
                    this.avanzarHacia(direccionPersonaje);
                }
            }
            if (puntoFinal.Y == Posicion.Y){
                while (posicion.X != puntoFinal.X){
                    this.avanzarHacia(direccionPersonaje);
                }
            }

            if ((puntoFinal.X == Posicion.X) && (puntoFinal.Y == Posicion.Y))
            {
                this.explotar();
            }

            return 1;

        }

}

}