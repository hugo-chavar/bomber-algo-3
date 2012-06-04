using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Arma
{
    public class LanzadorProyectil : ILanzador
    {

        public bool Lanzar(Punto posicion, int reduccionRetardo)
        {
            //chequear si Posicion valida???
            //Si no es valida return(false)
            Proyectil proyectil = new Proyectil(posicion);
            ManejadorProyectil manejador = new ManejadorProyectil(proyectil,1);
            // ACA HAY QUE IMPLEMENTAR EL LANZAR DEL PROYECTIL!
            return (true);//HARDCODEO PARA QUE ME FUNCIONE! ESTO ESTA MAL!
        }
    }
}
