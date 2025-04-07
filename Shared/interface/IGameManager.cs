using Shared.forme;

namespace Shared.Interface
{
    public interface IGameManager
    {
        bool PlacerForme(string position, Forme forme, int boardId = 1);
        bool ModifierForme(string position, Forme nouvelleForme, int boardId = 1);
        bool SupprimerForme(string position, int boardId = 1);
        Dictionary<string, object> ObtenirEchiquier(int boardId = 1);
        List<Board> GetAllBoards();
        Board CreateBoard(string name, string type = "standard");
    }
}