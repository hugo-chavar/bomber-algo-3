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
        protected int alcanceDelUltimoLanzamiento;

        public abstract bool Lanzar(Punto posicion, int reduccionRetardo);

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
            this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoVertical(this.alcanceDelUltimoLanzamiento);
            bool posicionCalculada = false;
            while (!this.posicionDeImpacto.Equals(this.posicionDeTiro) ||
                    !Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) || posicionCalculada)
            {
                this.alcanceDelUltimoLanzamiento--;
                this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoVertical(this.alcanceDelUltimoLanzamiento);
                if (Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) &&
                    Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).PermiteExplosivos())
                        
                        posicionCalculada = true;
            }
        }

        public void CalcularPosicionDeImpactoHaciaAbajo()
        {
            this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoVertical(this.alcanceDelUltimoLanzamiento * -1);
            bool posicionCalculada = false;
            while (!this.posicionDeImpacto.Equals(this.posicionDeTiro) ||
                    !Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) || posicionCalculada)
            {
                this.alcanceDelUltimoLanzamiento--;
                this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoVertical(this.alcanceDelUltimoLanzamiento * -1);
                if (Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) &&
                    Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).PermiteExplosivos())

                    posicionCalculada = true;
            }
        }

        public void CalcularPosicionDeImpactoHaciaLaIzquierda()
        {
            this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoHorizontal(this.alcanceDelUltimoLanzamiento * -1);
            bool posicionCalculada = false;
            while (!this.posicionDeImpacto.Equals(this.posicionDeTiro) ||
                    !Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) || posicionCalculada)
            {
                this.alcanceDelUltimoLanzamiento--;
                this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoHorizontal(this.alcanceDelUltimoLanzamiento * -1);
                if (Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) &&
                    Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).PermiteExplosivos())

                    posicionCalculada = true;
            }
        }

        public void CalcularPosicionDeImpactoHaciaLaDerecha()
        {
            this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoHorizontal(this.alcanceDelUltimoLanzamiento);
            bool posicionCalculada = false;
            while (!this.posicionDeImpacto.Equals(this.posicionDeTiro) ||
                    !Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) || posicionCalculada)
            {
                this.alcanceDelUltimoLanzamiento--;
                this.posicionDeImpacto = this.posicionDeTiro.MoverPosicionesEnSentidoHorizontal(this.alcanceDelUltimoLanzamiento);
                if (Juego.Juego.Instancia().Ambiente.ExisteCasillaEnPosicion(this.posicionDeImpacto) &&
                    Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).PermiteExplosivos())

                    posicionCalculada = true;
            }
        }

        public void CalcularPosicionDeImpacto()
        {
            //el lanzamiento puede hacerse por sobre obstaculos
            //pero el artefacto explosivo debe caer en un pasillo
            this.alcanceDelUltimoLanzamiento = this.Alcance;
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
