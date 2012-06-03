using Bomberman.Personaje;

namespace Bomberman.Mapa.Casilla
{
    public class BloqueComun:Obstaculo
    {

        private const int resistenciaLadrillos = 5;
        private const int resistenciaCemento = 10;

        private BloqueComun(Punto Posicion, int UnidadesResistencia)
            : base(UnidadesResistencia) // Saque la posicion de ak
        { } //: base(Posicion, UnidadesResistencia) // Nota: los obstaculos ya no necesitan posicion, son un State de Casilla

        public static BloqueComun CrearBloqueCemento(Punto posicion)
        { 
            BloqueComun BloqueCemento = new BloqueComun(posicion, resistenciaCemento); 
            return (BloqueCemento);
        }

        public static BloqueComun CrearBloqueLadrillos(Punto posicion)
        {
            BloqueComun BloqueLadrillos = new BloqueComun(posicion, resistenciaLadrillos); 
            return (BloqueLadrillos);
        }


        
    }
}
