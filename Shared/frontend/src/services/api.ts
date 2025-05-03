// src/services/api.ts
import axios from 'axios';

const API_URL = 'http://localhost:5106/api';

// Fonction pour récupérer l'échiquier avec les formes placées
export async function getBoard(boardId: number = 1) {
  try {
    // Récupérer la structure de l'échiquier avec les formes
    const response = await axios.get(`${API_URL}/game/board?boardId=${boardId}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de l'échiquier:", error);
    throw error;
  }
}

// Fonction pour récupérer les détails d'une forme à une position spécifique
export async function getFormeDetails(position: string, boardId: number = 1) {
  try {
    // Cette fonction serait implémentée côté backend pour récupérer les détails d'une forme à une position
    const response = await axios.get(`${API_URL}/game/forme/${position}?boardId=${boardId}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la récupération des détails de la forme à la position ${position}:`, error);
    throw error;
  }
}

// Fonction pour récupérer la liste des échiquiers
export async function getAllBoards() {
  try {
    const response = await axios.get(`${API_URL}/game/boards`);
    
    // Vérifier si la réponse est bien un tableau
    if (response.data === null || response.data === undefined) {
      console.warn("La réponse de l'API est null ou undefined");
      return [];
    }
    
    // Log pour déboguer
    console.log("Réponse de l'API getAllBoards:", response.data);
    console.log("Type de la réponse:", typeof response.data);
    
    // Vérifier si c'est un tableau, sinon tenter de convertir
    if (Array.isArray(response.data)) {
      return response.data;
    } else if (typeof response.data === 'object') {
      console.warn("La réponse n'est pas un tableau, tentative de conversion");
      const asArray: any[] = [];
      for (const key in response.data) {
        if (response.data.hasOwnProperty(key)) {
          asArray.push(response.data[key]);
        }
      }
      return asArray;
    } else {
      console.warn("La réponse n'est ni un tableau ni un objet:", response.data);
      return [];
    }
  } catch (error) {
    console.error("Erreur lors de la récupération des échiquiers:", error);
    return [];
  }
}

// Fonction pour créer un nouvel échiquier
export async function createBoard(name: string, type: string = 'standard') {
  try {
    const response = await axios.post(`${API_URL}/game/board`, { name, type });
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création de l'échiquier:", error);
    throw error;
  }
}

// Fonction pour placer une forme sur l'échiquier
export async function placeForme(position: string, type: string, boardId: number = 1, params: any = {}) {
  try {
    const formeData = {
      Type: type,
      ...params
    };
    
    const response = await axios.post(
      `${API_URL}/game/${position}?boardId=${boardId}`, 
      formeData
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors du placement de la forme:", error);
    throw error;
  }
}

// Fonction pour supprimer une forme de l'échiquier
export async function deleteForme(position: string, boardId: number = 1) {
  try {
    const response = await axios.delete(
      `${API_URL}/game/${position}?boardId=${boardId}`
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la suppression de la forme:", error);
    throw error;
  }
}