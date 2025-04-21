// src/ChessBoard.tsx
import React, { useEffect, useState } from "react";
import axios from 'axios';
import "./ChessBoard.css";

const API_URL = 'http://localhost:5106/api';

// Fonction pour récupérer l'échiquier avec les formes placées
async function getBoard(boardId: number = 1) {
  try {
    // Récupérer la structure de l'échiquier avec les formes
    const response = await axios.get(`${API_URL}/game/board?boardId=${boardId}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de l'échiquier:", error);
    throw error;
  }
}

// Fonction pour récupérer la liste des échiquiers
async function getAllBoards() {
  try {
    const response = await axios.get(`${API_URL}/game/boards`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des échiquiers:", error);
    return [];
  }
}

// Type pour représenter l'échiquier
type ChessBoardData = {
  [position: string]: string | null;
};

// Type pour les échiquiers disponibles
type BoardItem = {
  id: number;
  name: string;
  type: string;
  createdAt: string;
};

function ChessBoard() {
  const [boardData, setBoardData] = useState<ChessBoardData>({});
  const [boards, setBoards] = useState<BoardItem[]>([]);
  const [selectedBoardId, setSelectedBoardId] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  // Fonction pour générer les coordonnées de l'échiquier
  const generateChessPositions = () => {
    const positions: string[] = [];
    const letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];
    
    for (let i = 0; i < 8; i++) {
      for (let j = 0; j < 8; j++) {
        positions.push(`${letters[j]}${8 - i}`);
      }
    }
    
    return positions;
  };

  // Chargement de l'échiquier
  const loadBoard = async (boardId: number) => {
    try {
      setLoading(true);
      setError(null);
      // Récupérer les données directement de l'API sans transformation
      const data = await getBoard(boardId);
      console.log('Données de l\'échiquier reçues :', data);
      setBoardData(data);
    } catch (err) {
      setError("Erreur lors du chargement de l'échiquier");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  // Chargement des échiquiers disponibles
  const loadBoards = async () => {
    try {
      const data = await getAllBoards();
      setBoards(data);
    } catch (err) {
      console.error("Erreur lors du chargement des échiquiers:", err);
    }
  };

  // Changement d'échiquier
  const handleBoardChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const boardId = parseInt(event.target.value);
    setSelectedBoardId(boardId);
    loadBoard(boardId);
  };

  // Effet pour charger les données au montage du composant
  useEffect(() => {
    Promise.all([loadBoard(selectedBoardId), loadBoards()]);
  }, []);

  // Rendu d'une forme géométrique basé uniquement sur le type
  const renderShape = (type: string | null) => {
    if (!type) return null;
    
    // Utiliser directement le type renvoyé par l'API
    const lowerType = type.toLowerCase();
    
    return (
      <div className={`shape ${lowerType}`} title={type}>
        <div className={lowerType}></div>
      </div>
    );
  };

  if (loading) return <div className="loading">Chargement de l'échiquier...</div>;
  if (error) return <div className="error">{error}</div>;

  const positions = generateChessPositions();

  return (
    <div className="chessboard-container">
      <div className="board-selector">
        <label htmlFor="board-select">Choisir un échiquier: </label>
        <select 
          id="board-select" 
          value={selectedBoardId} 
          onChange={handleBoardChange}
        >
          {boards.map(board => (
            <option key={board.id} value={board.id}>
              {board.name}
            </option>
          ))}
        </select>
      </div>

      <div className="chessboard">
        {positions.map((position, index) => {
          const row = Math.floor(index / 8);
          const col = index % 8;
          const isLight = (row + col) % 2 === 0;
          // Récupérer directement le type de forme à cette position
          const forme = boardData[position];

          return (
            <div 
              key={position} 
              className={`chess-square ${isLight ? "light" : "dark"}`}
              data-position={position}
            >
              {renderShape(forme)}
              <div className="position-label">{position}</div>
            </div>
          );
        })}
      </div>
    </div>
  );
}

export default ChessBoard;