// src/GeometricShapes.ts
    import { Cercle as CercleInterface, Carre as CarreInterface, Rectangle as RectangleInterface, Triangle as TriangleInterface, FormeBase } from './types/shapes';
    
    export class Cercle implements CercleInterface {
        rayon: number;
        position: string;
        type: string;
        boardId: number;
    
        constructor(rayon: number, position: string, boardId: number) {
            this.rayon = rayon;
            this.position = position;
            this.type = 'cercle';
            this.boardId = boardId;
        }
    
        getAire(): number {
            return Math.PI * Math.pow(this.rayon, 2);
        }
    
        getPerimetre(): number {
            return 2 * Math.PI * this.rayon;
        }
    }
    
    export class Carre implements CarreInterface {
        cote: number;
        position: string;
        type: string;
        boardId: number;
    
        constructor(cote: number, position: string, boardId: number) {
            this.cote = cote;
            this.position = position;
            this.type = 'carre';
            this.boardId = boardId;
        }
    
        getAire(): number {
            return Math.pow(this.cote, 2);
        }
    
        getPerimetre(): number {
            return 4 * this.cote;
        }
    }
    
    export class Rectangle implements RectangleInterface {
        longueur: number;
        largeur: number;
        position: string;
        type: string;
        boardId: number;
    
        constructor(longueur: number, largeur: number, position: string, boardId: number) {
            this.longueur = longueur;
            this.largeur = largeur;
            this.position = position;
            this.type = 'rectangle';
            this.boardId = boardId;
        }
    
        getAire(): number {
            return this.longueur * this.largeur;
        }
    
        getPerimetre(): number {
            return 2 * (this.longueur + this.largeur);
        }
    }
    
    export class Triangle implements TriangleInterface {
        base: number;
        hauteur: number;
        position: string;
        type: string;
        boardId: number;
    
        constructor(base: number, hauteur: number, position: string, boardId: number) {
            this.base = base;
            this.hauteur = hauteur;
            this.position = position;
            this.type = 'triangle';
            this.boardId = boardId;
        }
    
        getAire(): number {
            return (this.base * this.hauteur) / 2;
        }
    
        getPerimetre(): number {
            const cote = Math.sqrt(Math.pow(this.base / 2, 2) + Math.pow(this.hauteur, 2));
            return this.base + 2 * cote;
        }
    }