﻿using System;
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

        public LanzadorProyectil()
        {
            this.Alcance = ALCANCELANZAMIENTO;
        }
        
        //public override bool Lanzar(Punto posicion, int reduccionRetardo)
        //{
        //    //chequear si Posicion valida???
        //    //Si no es valida return(false)
        //    Proyectil proyectil = new Proyectil(posicion);
        //    ManejadorProyectil manejador = new ManejadorProyectil(proyectil, 1);

        //    return (true);//HARDCODEO PARA QUE ME FUNCIONE! ESTO ESTA MAL!
        //}

        public override void Disparar()
        {
            Proyectil proyectil = new Proyectil(this.posicionDeImpacto);
        }


    }
}
