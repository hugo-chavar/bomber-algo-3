using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class Proyectil:Explosivo{

        

        // Buscar ENUMERADOS: ARRIBA = 1; ABAJO = 0; DERECHA = 2; IZQUIERDA = 3 //

    
         // establezco arbitrariamente un alcance de 3 casilleros (cuanto avanza el proytectil antes de explotar) y explota //
        private Punto posicionFinal;
        private int poderDeDestruccion;
        private int alcance = 3;
        

         public Proyectil(int x, int y)

        {
            this.poderDeDestruccion = 5;
            this.ondaExpansiva = 3;
            Punto PosicionFinal = new Punto(0, 0);
            Punto PosicionInicial = new Punto(x, y);
            posicionFinal = PosicionFinal;
            posicion = PosicionInicial;

        }

    

        public override void Daniar(IDaniable daniable)
        {
            daniable.DaniarConProyectil();

                                                                        // cuando explota genera el mismo danio que la tole tole //
        }
        public int Alcance
        {
            get { return this.alcance; }
            set { this.alcance = value; }
        }

         public Punto PosicionFinal
        {
            get { return this.posicionFinal; }
            set { this.posicionFinal = value; }
        }

        
         public Punto PosicionInicial
        {
            get { return this.posicion; }
            set { this.PosicionInicial = value; }
        }

        public void lanzarMisil(int direccionPersonaje)
        {
            ManejadorProyectil unManejador = new ManejadorProyectil(this,direccionPersonaje);
            unManejador.lanzarMisil();

        }
        


       /* private Punto CalcularZonaImpacto (int direccionPersonaje)
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

        public void AvanzarHacia(int direccion)
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

        public int DespegarProyectil(int direccionPersonaje)
        {
            Punto puntoFinal;
            puntoFinal = this.CalcularZonaImpacto(direccionPersonaje);   //falta chequear que los valores X e Y finales no sean negativos, pero creo que otra clase en un nivel mas arriba deberia implementar eso //
            if (puntoFinal.X == Posicion.X)
            {
                while (posicion.Y != puntoFinal.Y)
                {
                    this.AvanzarHacia(direccionPersonaje);
                }
            }
            if (puntoFinal.Y == Posicion.Y){
                while (posicion.X != puntoFinal.X){
                    this.AvanzarHacia(direccionPersonaje);
                }
            }

            if ((puntoFinal.X == Posicion.X) && (puntoFinal.Y == Posicion.Y))
            {
                this.Explotar();
            }

            return 1;

        }*/
     
}

}