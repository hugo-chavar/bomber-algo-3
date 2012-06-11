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

        public LanzadorProyectil()
        {
            this.Alcance = ALCANCELANZAMIENTO;
        }

        public override void Cargar(IMovible movil)
        {
            base.Cargar(movil);
            Proyectil unProyectil = new Proyectil(this.PosicionDeTiro.Clonar());
            unProyectil.Movimiento.Direccion = movil.Movimiento.Direccion;
            unProyectil.Alcance = ALCANCELANZAMIENTO;
            this.carga = unProyectil;
        }

        public override void Disparar()
        {
            Juego.Juego.Instancia().ObjetoContundenteLanzado(this.carga);
        }
    }
}
