using EzChess.forme;

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

    public void PlacerForme(string position, Forme forme)
    {
        if (_chessBoard != null)
        {
            _chessBoard.AjouterForme(forme);
        }
        else if (_echiquier.ContainsKey(position))
        {
            _echiquier[position] = forme;
        }
    }

    public void AfficherEchiquier()
    {
        string lettres = "ABCDEFGH";
        for (int i = 8; i >= 1; i--)
        {
            for (int j = 0; j < 8; j++)
            {
                string position = $"{lettres[j]}{i}";
                Console.Write(_echiquier[position] == null ? "[ ] " : "[X] ");
            }
            Console.WriteLine();
        }
    }

    public Forme? GetForme(string position)
    {
        if (_echiquier.ContainsKey(position))
        {
            return _echiquier[position];
        }
        return null;
    }
}

    

