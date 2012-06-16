using Bomberman.Personaje;
namespace Bomberman
{
    public interface IDaniable
    {
        void DaniarConBombaToleTole();
        void DaniarConBombaMolotov(int UnidadesDaniadas);
        void DaniarConProyectil(int UnidadesDaniadas);
        //void DaniarPorColision(int UnidadesDaniadas);
        bool Destruido();
    }
}
