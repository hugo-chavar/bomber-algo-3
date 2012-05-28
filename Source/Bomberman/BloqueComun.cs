namespace Bomberman
{
    public class BloqueComun:Obstaculo
    {
        private BloqueComun(Punto Posicion, int UnidadesResistencia)
            : base(Posicion, UnidadesResistencia)
        { }

        public static BloqueComun crearBloqueCemento(Punto posicion)
        { 
            BloqueComun BloqueCemento= new BloqueComun(posicion,10);
            return (BloqueCemento);
        }

        public static BloqueComun crearBloqueLadrillos(Punto posicion)
        {
            BloqueComun BloqueLadrillos = new BloqueComun(posicion, 5);
            return (BloqueLadrillos);
        }
        
    }
}
