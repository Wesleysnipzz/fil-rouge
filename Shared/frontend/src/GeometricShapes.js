// GeometricShapes.js
class Cercle {
    constructor(rayon) {
        this.rayon = rayon;
    }

    // Aire : π * r²
    getAire() {
        return Math.PI * Math.pow(this.rayon, 2);
    }

    // Périmètre : 2 * π * r
    getPerimetre() {
        return 2 * Math.PI * this.rayon;
    }
}

class Carre {
    constructor(cote) {
        this.cote = cote;
    }

    // Aire : côté²
    getAire() {
        return Math.pow(this.cote, 2);
    }

    // Périmètre : 4 * côté
    getPerimetre() {
        return 4 * this.cote;
    }
}

class Rectangle {
    constructor(longueur, largeur) {
        this.longueur = longueur;
        this.largeur = largeur;
    }

    // Aire : longueur * largeur
    getAire() {
        return this.longueur * this.largeur;
    }

    // Périmètre : 2 * (longueur + largeur)
    getPerimetre() {
        return 2 * (this.longueur + this.largeur);
    }
}

class Triangle {
    constructor(base, hauteur) {
        this.base = base;
        this.hauteur = hauteur;
    }

    // Aire : (base * hauteur) / 2
    getAire() {
        return (this.base * this.hauteur) / 2;
    }

    // Périmètre : base + 2 * (côté) (approximation)
    getPerimetre() {
        const cote = Math.sqrt(Math.pow(this.base / 2, 2) + Math.pow(this.hauteur, 2)); // Pythagore
        return this.base + 2 * cote;
    }
}

// Exporter les formes
export { Cercle, Carre, Rectangle, Triangle };
