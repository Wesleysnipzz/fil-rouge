// src/services/api.ts
import { FormeBase, Board } from '../types/shapes';

export async function getBoard(boardId: number = 1): Promise<Board> {
  const response = await fetch(`http://localhost:5106/api/game/board/${boardId}`);
  return response.json();
}

export async function getForms(): Promise<FormeBase[]> {
  const response = await fetch(`http://localhost:5106/Formes`);
  return response.json();
}

export async function placeForme(position: string, forme: FormeBase, boardId: number = 1): Promise<boolean> {
  const response = await fetch(`http://localhost:5106/api/game/forme/${boardId}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ position, forme })
  });
  return response.json();
}