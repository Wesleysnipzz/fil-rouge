using EzChess.forme;
using System.Text;

namespace EzChess;

public class GameManager
{
    private readonly Dictionary<string, Forme?> _echiquier;
    private readonly ChessBoard? _chessBoard;

    public GameManager(Dictionary<string, Forme?>? echiquier = null, ChessBoard? chessBoard = null)
    {
        _echiquier = echiquier ?? new Dictionary<string, Forme?>();
        _chessBoard = chessBoard;
        InitialiserEchiquier();
    }

    private void InitialiserEchiquier()
    {
        string lettres = "ABCDEFGH";
        for (int i = 1; i <= 8; i++)
        {
            foreach (char lettre in lettres)
            {
                string position = $"{lettre}{i}";
                _echiquier[position] = null;
            }
        }
    }

    public bool PlacerForme(string position, Forme forme) // place la forme dans gamemanager 
    {
        // Vérifier si la position existe
        if (!_echiquier.ContainsKey(position))
        {
            Console.WriteLine($"Échec du placement: La position {position} n'existe pas.");
            return false;
        }

        // Vérifier si la case est déjà occupée
        if (_echiquier[position] != null)
        {
            Console.WriteLine($"Échec du placement: La position {position} est déjà occupée par une forme {_echiquier[position]?.GetType().Name}.");
            return false;
        }

        // Placer la forme
        _echiquier[position] = forme;
    
        // Si ChessBoard existe, l'actualiser aussi
        _chessBoard?.AjouterForme(forme);
    
        // Message de confirmation
        Console.WriteLine($"Placement réussi: {forme.GetType().Name} placé à la position {position}.");
        return true;
    }

    public void AfficherEchiquier() // affiche l'échiquier à pas zapper 
    {
        string lettres = "ABCDEFGH";
        Console.WriteLine("  A B C D E F G H");
        Console.WriteLine(" +-----------------+");
    
        for (int i = 8; i >= 1; i--)
        {
            Console.Write($"{i}|");
            for (int j = 0; j < 8; j++)
            {
                string position = $"{lettres[j]}{i}";
                var forme = _echiquier[position];
            
                if (forme == null)
                    Console.Write(" ·");
                else
                {
                    char type = forme.GetType().Name[0];
                    Console.Write($"{type}{forme.GetValeur()}");
                }
            }
            Console.WriteLine($"|{i}");
        }
        Console.WriteLine(" +-----------------+");
        Console.WriteLine("  A B C D E F G H");
    }

    public Forme? GetForme(string position)
    {
        if (_echiquier.ContainsKey(position))
        {
            return _echiquier[position];
        }
        return null;
    }

    // --- Début des modifications ---

    public bool SupprimerForme(string position)
    {
        position = position.ToUpperInvariant();
        if (!_echiquier.ContainsKey(position))
        {
            Console.WriteLine($"La position {position} n'existe pas sur l'échiquier.");
            return false;
        }
        if (_echiquier[position] == null)
        {
            Console.WriteLine($"Aucune forme présente en {position} à supprimer.");
            return false;
        }
        var forme = _echiquier[position];
        _echiquier[position] = null;
        Console.WriteLine($"Forme {forme?.GetType().Name} supprimée de la position {position}.");
        return true;
    }

    public bool ModifierForme(string position, Forme nouvelleForme)
    {
        position = position.ToUpperInvariant();
        if (!_echiquier.ContainsKey(position))
        {
            Console.WriteLine($"La position {position} n'existe pas sur l'échiquier.");
            return false;
        }
        // On remplace la forme existante par la nouvelle
        _echiquier[position] = nouvelleForme;
        _chessBoard?.AjouterForme(nouvelleForme);
        Console.WriteLine($"Forme modifiée à la position {position} par {nouvelleForme.GetType().Name}.");
        return true;
    }

    public string ObtenirEchiquier()
    {
        StringBuilder sb = new StringBuilder();
        string lettres = "ABCDEFGH";
        sb.AppendLine("  A B C D E F G H");
        sb.AppendLine(" +-----------------+");

        for (int i = 8; i >= 1; i--)
        {
            sb.Append($"{i}|");
            for (int j = 0; j < 8; j++)
            {
                string position = $"{lettres[j]}{i}";
                var forme = _echiquier[position];
                if (forme == null)
                    sb.Append(" ·");
                else
                {
                    char type = forme.GetType().Name[0];
                    sb.Append($"{type}{forme.GetValeur()}");
                }
            }
            sb.AppendLine($"|{i}");
        }
        sb.AppendLine(" +-----------------+");
        sb.AppendLine("  A B C D E F G H");
        return sb.ToString();
    }
    // --- Fin des modifications ---
}
