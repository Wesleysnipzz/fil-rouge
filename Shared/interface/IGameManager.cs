
using Shared.forme;

namespace Shared.Interface
{
    public interface IGameManager
    {
        bool PlacerForme(string position, Forme forme);
        bool ModifierForme(string position, Forme nouvelleForme);
        bool SupprimerForme(string position);
        Dictionary<string, object> ObtenirEchiquier();
    }
}