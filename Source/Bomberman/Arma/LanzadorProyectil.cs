using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Personaje;
using Bomberman.Mapa;

namespace Bomberman.Arma
{
    public class LanzadorProyectil : Lanzador
    {

        private const int ALCANCELANZAMIENTO = 3;
        private List<Punto> recorridoProyectil;

        public LanzadorProyectil()
        {
            this.Alcance = ALCANCELANZAMIENTO;
            this.sentido = new Movimiento();
            
        }
        
        //public override bool Lanzar(Punto posicion, int reduccionRetardo)
        //{
        //    //chequear si Posicion valida???
        //    //Si no es valida return(false)
        //    Proyectil proyectil = new Proyectil(posicion);
        //    ManejadorProyectil manejador = new ManejadorProyectil(proyectil, 1);

        //    return (true);//HARDCODEO PARA QUE ME FUNCIONE! ESTO ESTA MAL!
        //}

        public override void Apuntar(IMovible movil)
        {
            this.recorridoProyectil = new List<Punto>();
            base.Apuntar(movil);
        }

        public override void Disparar()
        {
            Proyectil unProyectil = new Proyectil(this.posicionDeImpacto);
            unProyectil.Trayectoria = this.recorridoProyectil;
            //Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).Transitar(unProyectil); habilitar esto cuando martin termine con las interfaces
            //Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).PlantarExplosivo(); esto no va
        }

        public override void CalcularPosicionDeImpactoHaciaArriba()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionSuperior();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                //la posAux es el destino o esta en el recorrido del proyectil
                this.posicionDeImpacto = posAux;
                this.recorridoProyectil.Add(posAux);
                posAux = this.posicionDeImpacto.PosicionSuperior();
                i--;
            }
        }

        public override void CalcularPosicionDeImpactoHaciaAbajo()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionInferior();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                //la posAux es el destino o esta en el recorrido del proyectil
                this.posicionDeImpacto = posAux;
                this.recorridoProyectil.Add(posAux);
                posAux = this.posicionDeImpacto.PosicionInferior();
                i--;
            }
        }

        public override void CalcularPosicionDeImpactoHaciaLaIzquierda()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionIzquierda();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                //la posAux es el destino o esta en el recorrido del proyectil
                this.posicionDeImpacto = posAux;
                this.recorridoProyectil.Add(posAux);
                posAux = this.posicionDeImpacto.PosicionIzquierda();
                i--;
            }
        }

        public override void CalcularPosicionDeImpactoHaciaLaDerecha()
        {
            //sabemos que la posicion en la que esta es un pasillo y puede dejar la bomba alli
            this.posicionDeImpacto = this.posicionDeTiro.Clonar();
            //voy hasta el alcance del lanzamiento o hasta un obstaculo o fin de mapa
            int i = this.Alcance;
            Punto posAux = this.posicionDeTiro.PosicionDerecha();
            while (i > 0 && Juego.Juego.Instancia().Ambiente.PermitidoLanzarExplosivoAPos(posAux))
            {
                //la posAux es el destino o esta en el recorrido del proyectil
                this.posicionDeImpacto = posAux;
                this.recorridoProyectil.Add(posAux);
                posAux = this.posicionDeImpacto.PosicionDerecha();
                i--;
            }
        }
    }
}
