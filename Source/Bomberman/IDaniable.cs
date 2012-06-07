using Bomberman.Personaje;
namespace Bomberman
{
    public interface IDaniable// : IMovible
    {
        void DaniarConBombaToleTole();
        void DaniarConBombaMolotov(int UnidadesDaniadas);
        void DaniarConProyectil(int UnidadesDaniadas);
        bool Destruido();
    }
}
