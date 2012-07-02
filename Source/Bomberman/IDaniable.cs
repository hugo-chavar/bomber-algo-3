using BombermanModel.Personaje;
namespace BombermanModel
{
    public interface IDaniable
    {
        void DaniarConBombaToleTole();
        void DaniarConBombaMolotov(int UnidadesDaniadas);
        void DaniarConProyectil(int UnidadesDaniadas);
        bool Destruido();
    }
}
