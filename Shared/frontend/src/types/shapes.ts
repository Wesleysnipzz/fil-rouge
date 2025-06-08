// src/types/shapes.ts

export interface FormeBase {
  position: string;
  type: string;
  boardId: number;
}

export interface Cercle extends FormeBase {
  rayon: number;
}

export interface Carre extends FormeBase {
  cote: number;
}

export interface Rectangle extends FormeBase {
  longueur: number;
  largeur: number;
}

export interface Triangle extends FormeBase {
  base: number;
  hauteur: number;
}

export type Forme = Cercle | Carre | Rectangle | Triangle;

export interface Board {
  id: number;
  name: string;
  type: string;
  createdAt: string;
}