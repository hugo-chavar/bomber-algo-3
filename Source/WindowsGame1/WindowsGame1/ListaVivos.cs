using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BombermanModel;
using BombermanModel.Arma;
using BombermanModel.Juego;

namespace BombermanGame
{
    class ListaVivos
    {
        public static List<ObjetoVivo> objList = new List<ObjetoVivo>();

        public static void Initialize()
        {
            //objList.Add(new Pared(new Vector2(100, 100)));
            objList.Add(new BombitaView(new Vector2(200, 200)));
            objList.Add(new LopezReggaeAladoView(new Vector2(200, 200)));
            objList.Add(new CecilioView(new Vector2(356, 300)));
            objList.Add(new LopezReggaeView(new Vector2(356, 300)));
            Proyectil unProyectil = new Proyectil(new Punto(3, 0));
            unProyectil.Movimiento.Direccion = 6;
            objList.Add(new ProyectilView(new Vector2(200, 200), unProyectil));
            Juego.Instancia().DependientesDelTiempo.Add(unProyectil);
        }
    }
}
