using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;

namespace Bomberman.Arma
{
    public abstract class Lanzador
    {
        protected Movimiento sentido;
        protected Punto posicionDeTiro;
        protected int retardoExplosion;
        protected Punto posicionDeImpacto;
        protected int alcance;

        public abstract void Disparar(); //Lanzar(Punto posicion, int reduccionRetardo)--- reemplazo de metodo

        public int Alcance
        {
            get { return this.alcance; }
            set { this.alcance = value; }
        }

        public int RetardoExplosion
        {
            get { return this.retardoExplosion; }
            set { this.retardoExplosion = value; }
        }

        public Punto PosicionDeTiro
        {
            get { return this.posicionDeTiro; }
            set { this.posicionDeTiro = value; }
        }

        public Movimiento Sentido
        {
            get { return this.sentido; }
            set { this.sentido = value; }
        }

        public void CalcularPosicionDeImpactoHaciaArriba()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionSuperior();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                this.posicionDeImpacto = posAux;
                posAux = this.posicionDeImpacto.PosicionSuperior();
                i--;
            }
        }

        public void CalcularPosicionDeImpactoHaciaAbajo()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionInferior();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                this.posicionDeImpacto = posAux;
                posAux = this.posicionDeImpacto.PosicionInferior();
                i--;
            }
        }

        public void CalcularPosicionDeImpactoHaciaLaIzquierda()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionIzquierda();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                this.posicionDeImpacto = posAux;
                posAux = this.posicionDeImpacto.PosicionIzquierda();
                i--;
            }
        }

        public void CalcularPosicionDeImpactoHaciaLaDerecha()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionDerecha();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                this.posicionDeImpacto = posAux;
                posAux = this.posicionDeImpacto.PosicionDerecha();
                i--;
            }
        }

        public void CalcularPosicionDeImpacto()
        {
            //el lanzamiento no puede hacerse por sobre obstaculos
            //el artefacto explosivo debe caer en un pasillo
            switch (this.Sentido.Direccion)
            {
                case Mapa.Mapa.ARRIBA:
                    {
                        CalcularPosicionDeImpactoHaciaArriba();
                        break;
                    }
                case Mapa.Mapa.ABAJO:
                    {
                        CalcularPosicionDeImpactoHaciaAbajo();
                        break;
                    }
                case Mapa.Mapa.IZQUIERDA:
                    {
                        CalcularPosicionDeImpactoHaciaLaIzquierda();
                        break;
                    }
                case Mapa.Mapa.DERECHA:
                    {
                        CalcularPosicionDeImpactoHaciaLaDerecha();
                        break;
                    }
            }

        }
    }

}
