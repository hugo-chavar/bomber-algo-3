﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Juego;
using Bomberman.Mapa.Casilla;
using Bomberman.Personaje;

namespace Bomberman.Arma
{
    public class LanzadorMolotov:Lanzador
    {

        private const int ALCANCELANZAMIENTO = 0;

        public LanzadorMolotov()
        {
            this.Alcance = ALCANCELANZAMIENTO;
            this.sentido = new Movimiento();
        }

        //public override bool Lanzar(Punto posicion, int reduccionRetardo)  //Hugo dice: dejo comentado esto a efectos didácticos lo reemplacé por Disparar()
        //{
        //    Casilla casilla = Juego.Juego.Instancia().Ambiente.ObtenerCasilla(posicion);
        //    if (casilla.Explosivo==null)
        //    {
        //        Bomba bomba = new BombaMolotov(posicion, reduccionRetardo);
        //        casilla.PlantarExplosivo(bomba);
        //        return (true); //Las bombas se ponen en la posicion del personaje
        //    }
        //    return (false); //casilla ya ocupada Con Bomba
        //}

        public override void Disparar()
        {
            Juego.Juego.Instancia().Ambiente.ObtenerCasilla(this.posicionDeImpacto).PlantarExplosivo(new BombaMolotov(this.posicionDeImpacto, this.RetardoExplosion));
        }
    }
}
