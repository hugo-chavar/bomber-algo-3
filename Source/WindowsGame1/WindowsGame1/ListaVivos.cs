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
using BombermanModel.Personaje;

namespace BombermanGame
{
    class ListaVivos
    {
        public static List<ObjetoVivo> objList = new List<ObjetoVivo>();

        public static void Initialize()
        {
            ///objList.Add(new Pared(new Vector2(100, 100)));
            //objList.Add(new BombitaView());
            objList.Add(new LopezReggaeAladoView(new LosLopezReggaeAlado(new Punto(10, 10))));
            objList.Add(new CecilioView(new Cecilio(new Punto(10, 5))));
            objList.Add(new LopezReggaeView(new LosLopezReggae(new Punto(13, 6))));
            //objList.Add(new ProyectilView(new Vector2(356, 300)));

            for (int i = 0; i < 10; i++)
            {
                objList.Add(new ProyectilView());
            }
        }

        public static void AgregarDibujable(ObjetoVivo unDibujable)
        {

            objList.Add(unDibujable);

        }

        //usar esto al pasar de niveles, limpia los objetos q esten cargados
        public static void Reset()
        {
            foreach (ObjetoVivo o in objList)
                o.Vivo = false;
        }
    }
}
