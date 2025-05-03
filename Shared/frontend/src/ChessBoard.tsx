// src/ChessBoard.tsx
import React, { useEffect, useState,} from "react";
import axios from 'axios';
import "./ChessBoard.css";

const API_URL = 'http://localhost:5106/api';

// Fonction pour récupérer l'échiquier avec les formes placées
async function getBoard(boardId: number = 1) {
  try {
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

// Fonction pour créer un nouvel échiquier
async function createBoard(name: string, type: string = 'standard') {
  try {
    const response = await axios.post(`${API_URL}/game/board`, { name, type });
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création de l'échiquier:", error);
    throw error;
  }
}

// Fonction pour récupérer les détails d'une forme
async function getFormeDetails(position: string, boardId: number = 1) {
  try {
    const response = await axios.get(`${API_URL}/game/forme/${position}?boardId=${boardId}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la récupération des détails de la forme à ${position}:`, error);
    return null;
  }
}

// Fonction pour récupérer l'échiquier avec les détails des formes
async function getBoardWithDetails(boardId: number = 1) {
  try {
    const basicBoard = await getBoard(boardId);
    const detailedBoard: {[position: string]: any} = {};

    // Pour chaque position avec une forme, récupérer ses détails
    for (const position in basicBoard) {
      if (basicBoard[position] !== null) {
        try {
          const details = await getFormeDetails(position, boardId);
          detailedBoard[position] = {
            type: basicBoard[position],
            ...(details || {})
          };
        } catch {
          detailedBoard[position] = { type: basicBoard[position] };
        }
      } else {
        detailedBoard[position] = null;
      }
    }

    return detailedBoard;
  } catch (error) {
    console.error("Erreur lors de la récupération des détails de l'échiquier:", error);
    throw error;
  }
}

// Fonction pour ajouter une forme
async function addForme(formeData: any) {
  try {
    const response = await axios.post(
        `${API_URL}/game/${formeData.position}?boardId=${formeData.boardId}`,
        formeData
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de l'ajout de la forme:", error);
    throw error;
  }
}

// Fonction pour modifier une forme
async function updateForme(position: string, formeData: any, boardId: number = 1) {
  try {
    const response = await axios.put(
        `${API_URL}/game/${position}?boardId=${boardId}`,
        formeData
    );
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la modification de la forme à ${position}:`, error);
    throw error;
  }
}

// Fonction pour supprimer une forme
async function deleteForme(position: string, boardId: number = 1) {
  try {
    const response = await axios.delete(
        `${API_URL}/game/${position}?boardId=${boardId}`
    );
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la suppression de la forme à ${position}:`, error);
    throw error;
  }
}

// Type pour représenter les données d'une forme
type FormeData = {
  type: string;
  cote?: number;
  largeur?: number;
  hauteur?: number;
  rayon?: number;
  base?: number;
  [key: string]: any;
};

// Type pour représenter l'échiquier
type ChessBoardData = {
  [position: string]: FormeData | null;
};

// Type pour les échiquiers disponibles
type BoardItem = {
  id: number;
  name: string;
  type: string;
  createdAt: string;
};

// Type pour les données envoyées à l'API
interface FormeRequestData {
  type: string;
  position: string;
  boardId: number;
  cote?: number;
  longueur?: number;
  largeur?: number;
  rayon?: number;
  base?: number;
  hauteur?: number;
}

function ChessBoard() {
  const [boardData, setBoardData] = useState<ChessBoardData>({});
  const [boards, setBoards] = useState<BoardItem[]>([]);
  const [selectedBoardId, setSelectedBoardId] = useState<number>(1);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [newBoardName, setNewBoardName] = useState<string>("");
  const [creatingBoard, setCreatingBoard] = useState<boolean>(false);

  // Nouvel état pour suivre la position sélectionnée
  const [selectedPosition, setSelectedPosition] = useState<string | null>(null);

  // États pour le formulaire d'ajout de forme
  const [showAddForm, setShowAddForm] = useState<boolean>(false);
  const [addFormData, setAddFormData] = useState<any>({ type: 'carre', cote: 2 });
  const [addingForme, setAddingForme] = useState<boolean>(false);

  // États pour le formulaire d'édition de forme
  const [editForm, setEditForm] = useState<{visible: boolean, position: string | null}>({visible: false, position: null});
  const [editFormData, setEditFormData] = useState<any>({});
  const [editingForme, setEditingForme] = useState<boolean>(false);

  // États pour le formulaire de suppression de forme
  const [showDeleteForm, setShowDeleteForm] = useState<boolean>(false);

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

  // Fonction pour gérer le clic sur une case
  const handleSquareClick = (position: string) => {
    setSelectedPosition(position);
  };

  // Chargement de l'échiquier
  const loadBoard = async (boardId: number) => {
    try {
      setLoading(true);
      setError(null);

      // Récupérer toujours les données détaillées des formes
      const data = await getBoardWithDetails(boardId);
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
      console.log('Échiquiers reçus :', data);

      // Vérifier si data est bien un tableau
      if (Array.isArray(data)) {
        setBoards(data);
      } else if (data && typeof data === 'object') {
        // Si c'est un objet mais pas un tableau, on essaie de le convertir
        console.warn('Les données reçues ne sont pas un tableau mais un objet :', data);
        // Conversion manuelle de l'objet en tableau
        const asArray: BoardItem[] = [];
        for (const key of Object.keys(data)) {
          asArray.push((data as Record<string, any>)[key] as BoardItem);
        }
        if (asArray.length > 0) {
          console.info('Conversion en tableau réussie :', asArray);
          setBoards(asArray);
        } else {
          console.error('Impossible de convertir l\'objet en tableau');
          setBoards([]);
        }
      } else {
        console.error('Les données reçues ne sont ni un tableau ni un objet :', data);
        setBoards([]);
      }
    } catch (err) {
      console.error("Erreur lors du chargement des échiquiers:", err);
      setBoards([]);
    }
  };

  // Changement d'échiquier
  const handleBoardChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const boardId = parseInt(event.target.value);
    setSelectedBoardId(boardId);
    loadBoard(boardId);
    setSelectedPosition(null);
  };

  // Création d'un nouvel échiquier
  const handleCreateBoard = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!newBoardName.trim()) return;

    try {
      setCreatingBoard(true);
      await createBoard(newBoardName);
      setNewBoardName("");
      await loadBoards(); // Recharger la liste des échiquiers
    } catch (err) {
      console.error("Erreur lors de la création de l'échiquier:", err);
    } finally {
      setCreatingBoard(false);
    }
  };

  // Création rapide d'un échiquier de test
  const createTestBoard = async () => {
    try {
      setCreatingBoard(true);
      const testBoardName = "Échiquier-test-" + new Date().getTime();
      console.log("Création d'un échiquier de test:", testBoardName);
      const newBoard = await createBoard(testBoardName);
      console.log("Nouvel échiquier créé:", newBoard);
      await loadBoards(); // Recharger la liste des échiquiers

      // Sélectionner automatiquement le nouvel échiquier créé
      if (newBoard && newBoard.id) {
        setSelectedBoardId(newBoard.id);
        loadBoard(newBoard.id);
      }
    } catch (err) {
      console.error("Erreur lors de la création de l'échiquier de test:", err);
    } finally {
      setCreatingBoard(false);
    }
  };

  // Handler pour afficher le formulaire d'ajout
  const handleShowAddForm = () => {
    if (!selectedPosition || boardData[selectedPosition] !== null) return;

    // Réinitialiser le formulaire avec des valeurs par défaut
    setAddFormData({
      type: 'carre',
      cote: 2,
      rayon: 2,
      largeur: 3,
      hauteur: 2,
      base: 3
    });

    setShowAddForm(true);
  };

  // Handler pour cacher le formulaire d'ajout
  const handleHideAddForm = () => {
    setShowAddForm(false);
  };

  // Handler pour afficher le formulaire d'édition
  const handleShowEditForm = () => {
    if (!selectedPosition || !boardData[selectedPosition]) return;
    setEditForm({ visible: true, position: selectedPosition });
    setEditFormData({ ...boardData[selectedPosition] });
  };

  // Handler pour cacher le formulaire d'édition
  const handleHideEditForm = () => {
    setEditForm({ visible: false, position: null });
  };

  const handleAddForme = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!selectedPosition) return;

    setAddingForme(true);
    try {
      // Créer l'objet à envoyer selon le type sélectionné
      let dataToSend: FormeRequestData = {
        type: addFormData.type,
        position: selectedPosition,
        boardId: selectedBoardId
      };

      // Ajouter les propriétés spécifiques selon le type
      switch (addFormData.type) {
        case 'carre':
          dataToSend = { ...dataToSend, cote: addFormData.cote };
          break;
        case 'rectangle':
          dataToSend = { ...dataToSend, longueur: addFormData.longueur, largeur: addFormData.largeur };
          break;
        case 'cercle':
          dataToSend = { ...dataToSend, rayon: addFormData.rayon };
          break;
        case 'triangle':
          dataToSend = { ...dataToSend, cote: addFormData.base };
          break;
      }

      console.log("Données à envoyer:", dataToSend);
      await addForme(dataToSend);
      await loadBoard(selectedBoardId);
      handleHideAddForm();
    } catch (err) {
      console.error("Erreur lors de l'ajout de la forme:", err);
    } finally {
      setAddingForme(false);
    }
  };

  // Handler pour soumettre le formulaire d'édition
  const handleEditForme = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!editForm.position) return;
    setEditingForme(true);
    try {
      await updateForme(editForm.position, editFormData, selectedBoardId);
      await loadBoard(selectedBoardId);
      handleHideEditForm();
    } catch (err) {
      console.error("Erreur lors de la modification de la forme:", err);
    } finally {
      setEditingForme(false);
    }
  };

  // Handler pour supprimer une forme
  const handleDeleteForme = async () => {
    if (!selectedPosition || !boardData[selectedPosition]) return;

    try {
      console.log(`Suppression de la forme à ${selectedPosition} sur l'échiquier ${selectedBoardId}`);

      // Vérifier que la position est en majuscules pour correspondre au format du backend
      const formattedPosition = selectedPosition.toUpperCase();

      await deleteForme(formattedPosition, selectedBoardId);
      console.log('Forme supprimée, rechargement du tableau...');
      await loadBoard(selectedBoardId);
      setSelectedPosition(null);  // Réinitialiser la position sélectionnée après suppression
    } catch (err) {
      console.error("Erreur lors de la suppression de la forme:", err);
    }
  };
  // Effet pour charger les données au montage du composant
  useEffect(() => {
    Promise.all([loadBoard(selectedBoardId), loadBoards()]);
  }, []);

  // Rendu d'une forme géométrique basé sur le type et les dimensions
  const renderShape = (formeData: {type: string, [key: string]: any} | null) => {
    if (!formeData) return null;

    // Utiliser directement le type renvoyé par l'API
    const type = formeData.type.toLowerCase();

    // Définir la taille maximale de l'espace disponible
    const maxSize = 80; // en pixels

    // Dessiner la forme en SVG selon son type et ses dimensions
    if (type === 'carre') {
      const cote = formeData.cote || 5; // Valeur par défaut si non spécifiée
      const size = Math.min(maxSize, cote * 10); // Échelle: 10px par unité

      return (
          <svg width={maxSize} height={maxSize} viewBox={`0 0 ${maxSize} ${maxSize}`}>
            <rect
                x={(maxSize - size) / 2}
                y={(maxSize - size) / 2}
                width={size}
                height={size}
                fill="#f5f5f5"
                stroke="#aaaaaa"
                strokeWidth="1"
            />
          </svg>
      );
    }

    if (type === 'rectangle') {
      const largeur = formeData.largeur || 5;
      const hauteur = formeData.hauteur || 3;
      const width = Math.min(maxSize, largeur * 10);
      const height = Math.min(maxSize, hauteur * 10);

      return (
          <svg width={maxSize} height={maxSize} viewBox={`0 0 ${maxSize} ${maxSize}`}>
            <rect
                x={(maxSize - width) / 2}
                y={(maxSize - height) / 2}
                width={width}
                height={height}
                fill="#333333"
                stroke="none"
            />
          </svg>
      );
    }

    if (type === 'cercle') {
      const rayon = formeData.rayon || 2.5;
      const radius = Math.min(maxSize / 2, rayon * 10);

      return (
          <svg width={maxSize} height={maxSize} viewBox={`0 0 ${maxSize} ${maxSize}`}>
            <circle
                cx={maxSize / 2}
                cy={maxSize / 2}
                r={radius}
                fill="#f5f5f5"
                stroke="#aaaaaa"
                strokeWidth="1"
            />
          </svg>
      );
    }

    if (type === 'triangle') {
      const base = formeData.base || 5;
      const baseSize = Math.min(maxSize, base * 10);
      const height = (Math.sqrt(3) / 2) * baseSize; // Triangle équilatéral

      // Calculer les points du triangle
      const x1 = (maxSize - baseSize) / 2;
      const y1 = (maxSize + height) / 2;
      const x2 = x1 + baseSize;
      const y2 = y1;
      const x3 = maxSize / 2;
      const y3 = (maxSize - height) / 2;

      return (
          <svg width={maxSize} height={maxSize} viewBox={`0 0 ${maxSize} ${maxSize}`}>
            <polygon
                points={`${x1},${y1} ${x2},${y2} ${x3},${y3}`}
                fill="#333333"
                stroke="none"
            />
          </svg>
      );
    }

    // Forme non reconnue
    return (
        <div className="unknown-shape" title={formeData.type}>?</div>
    );
  };

  if (loading) return <div className="loading">Chargement de l'échiquier...</div>;
  if (error) return <div className="error">{error}</div>;

  const positions = generateChessPositions();

  return (
      <div className="chessboard-container">
        <div className="board-controls">
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
            <button
                type="button"
                onClick={createTestBoard}
                disabled={creatingBoard}
                className="test-board-button"
            >
              Créer un échiquier de test
            </button>
          </div>

          <div className="board-creator">
            <form onSubmit={handleCreateBoard}>
              <input
                  type="text"
                  placeholder="Nom du nouvel échiquier"
                  value={newBoardName}
                  onChange={(e) => setNewBoardName(e.target.value)}
                  required
              />
              <button type="submit" disabled={creatingBoard || !newBoardName.trim()}>
                {creatingBoard ? "Création..." : "Créer un échiquier"}
              </button>
            </form>
          </div>
        </div>

        <div className="chessboard-interface">
          <div className="chessboard">
            {positions.map((position, index) => {
              const row = Math.floor(index / 8);
              const col = index % 8;
              const isLight = (row + col) % 2 === 0;
              const forme = boardData[position];
              const isSelected = selectedPosition === position;

              return (
                  <div
                      key={position}
                      className={`chess-square ${isLight ? 'light' : 'dark'} ${isSelected ? 'selected' : ''}`}
                      onClick={() => handleSquareClick(position)}
                  >
                    {renderShape(forme)}
                    <div className="position-label">{position}</div>
                  </div>
              );
            })}
          </div>

          <div className="board-actions">
            <div className="selected-info">
              {selectedPosition ? (
                  <>
                    <h3>Position: {selectedPosition}</h3>
                    <p>
                      {boardData[selectedPosition]
                          ? `Forme: ${boardData[selectedPosition]?.type}`
                          : 'Aucune forme'}
                    </p>
                  </>
              ) : (
                  <p>Sélectionnez une case sur l'échiquier</p>
              )}
            </div>

            <div className="action-buttons">
              <button
                  className="add-button"
                  onClick={handleShowAddForm}
                  disabled={!selectedPosition || boardData[selectedPosition] !== null}
              >
                Ajouter une forme
              </button>

              <button
                  className="edit-button"
                  onClick={handleShowEditForm}
                  disabled={!selectedPosition || boardData[selectedPosition] === null}
              >
                Modifier la forme
              </button>

              <button
                  className="delete-button"
                  onClick={handleDeleteForme}
                  disabled={!selectedPosition || boardData[selectedPosition] === null}
              >
                Supprimer la forme
              </button>
            </div>
          </div>
        </div>

        {/* Formulaire d'ajout de forme */}
        {showAddForm && (
            <div className="add-forme-modal">
              <form className="add-forme-form" onSubmit={handleAddForme}>
                <h3>Ajouter une forme à {selectedPosition}</h3>
                <label>
                  Type:
                  <select value={addFormData.type} onChange={e => setAddFormData({ ...addFormData, type: e.target.value })}>
                    <option value="carre">Carré</option>
                    <option value="rectangle">Rectangle</option>
                    <option value="cercle">Cercle</option>
                    <option value="triangle">Triangle</option>
                  </select>
                </label>
                {/* Champs dynamiques selon le type */}
                {addFormData.type === 'carre' && (
                    <label>Côté: <input type="number" min={1} value={addFormData.cote || ''} onChange={e => setAddFormData({ ...addFormData, cote: Number(e.target.value) })} required /></label>
                )}
                {addFormData.type === 'rectangle' && (
                    <>
                      <label>Largeur: <input type="number" min={1} value={addFormData.largeur || ''} onChange={e => setAddFormData({ ...addFormData, largeur: Number(e.target.value) })} required /></label>
                      <label>Hauteur: <input type="number" min={1} value={addFormData.hauteur || ''} onChange={e => setAddFormData({ ...addFormData, hauteur: Number(e.target.value) })} required /></label>
                    </>
                )}
                {addFormData.type === 'cercle' && (
                    <label>Rayon: <input type="number" min={1} value={addFormData.rayon || ''} onChange={e => setAddFormData({ ...addFormData, rayon: Number(e.target.value) })} required /></label>
                )}
                {addFormData.type === 'triangle' && (
                    <>
                      <label>Base: <input type="number" min={1} value={addFormData.base || ''} onChange={e => setAddFormData({ ...addFormData, base: Number(e.target.value) })} required /></label>
                      <label>Hauteur: <input type="number" min={1} value={addFormData.hauteur || ''} onChange={e => setAddFormData({ ...addFormData, hauteur: Number(e.target.value) })} required /></label>
                    </>
                )}
                <div className="form-actions">
                  <button type="submit" disabled={addingForme}>{addingForme ? "Ajout..." : "Ajouter"}</button>
                  <button type="button" onClick={handleHideAddForm}>Annuler</button>
                </div>
              </form>
            </div>
        )}

        {/* Formulaire d'édition de forme */}
        {editForm.visible && (
            <div className="edit-forme-modal">
              <form className="edit-forme-form" onSubmit={handleEditForme}>
                <h3>Modifier la forme à {editForm.position}</h3>
                <label>
                  Type:
                  <select value={editFormData.type} onChange={e => setEditFormData({ ...editFormData, type: e.target.value })}>
                    <option value="carre">Carré</option>
                    <option value="rectangle">Rectangle</option>
                    <option value="cercle">Cercle</option>
                    <option value="triangle">Triangle</option>
                  </select>
                </label>
                {/* Champs dynamiques selon le type */}
                {editFormData.type === 'carre' && (
                    <label>Côté: <input type="number" min={1} value={editFormData.cote || ''} onChange={e => setEditFormData({ ...editFormData, cote: Number(e.target.value) })} required /></label>
                )}
                {editFormData.type === 'rectangle' && (
                    <>
                      <label>Largeur: <input type="number" min={1} value={editFormData.largeur || ''} onChange={e => setEditFormData({ ...editFormData, largeur: Number(e.target.value) })} required /></label>
                      <label>Hauteur: <input type="number" min={1} value={editFormData.hauteur || ''} onChange={e => setEditFormData({ ...editFormData, hauteur: Number(e.target.value) })} required /></label>
                    </>
                )}
                {editFormData.type === 'cercle' && (
                    <label>Rayon: <input type="number" min={1} value={editFormData.rayon || ''} onChange={e => setEditFormData({ ...editFormData, rayon: Number(e.target.value) })} required /></label>
                )}
                {editFormData.type === 'triangle' && (
                    <>
                      <label>Base: <input type="number" min={1} value={editFormData.base || ''} onChange={e => setEditFormData({ ...editFormData, base: Number(e.target.value) })} required /></label>
                      <label>Hauteur: <input type="number" min={1} value={editFormData.hauteur || ''} onChange={e => setEditFormData({ ...editFormData, hauteur: Number(e.target.value) })} required /></label>
                    </>
                )}
                <div className="form-actions">
                  <button type="submit" disabled={editingForme}>{editingForme ? "Modification..." : "Modifier"}</button>
                  <button type="button" onClick={handleHideEditForm}>Annuler</button>
                </div>
              </form>
            </div>
        )}
      </div>
  );
}

export default ChessBoard;
