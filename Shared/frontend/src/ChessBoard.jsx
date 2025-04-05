import React, { useEffect, useState } from "react";
import { Cercle, Carre, Rectangle, Triangle } from './GeometricShapes'; // Importer les formes géométriques
import "./ChessBoard.css";

function ChessBoard() {
    const [board, setBoard] = useState({});
    const [formesMap, setFormesMap] = useState(new Map());
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setLoading(true);

        // Récupérer l'échiquier et les formes en parallèle
        Promise.all([
            fetch("http://localhost:5106/api/game/board").then(res => res.json()),
            fetch("http://localhost:5106/Formes").then(res => res.json())
        ])
            .then(([boardData, formesData]) => {
                console.log("Board récupéré :", boardData);
                console.log("Formes récupérées :", formesData);

                setBoard(boardData);

                // Map rapide pour associer position -> forme (ex: "A1" -> forme Cercle)
                const formesMap = new Map(
                    formesData.map(f => {
                        let forme;
                        switch (f.type?.toLowerCase()) {
                            case "cercle":
                                forme = new Cercle(f.rayon); // f.rayon doit être passé en paramètre
                                break;
                            case "carre":
                                forme = new Carre(f.cote); // f.cote pour le carré
                                break;
                            case "rectangle":
                                forme = new Rectangle(f.longueur, f.largeur); // f.longueur et f.largeur
                                break;
                            case "triangle":
                                forme = new Triangle(f.base, f.hauteur); // f.base et f.hauteur
                                break;
                            default:
                                forme = null; // Si aucune forme correspondante
                        }
                        return [f.position, forme]; // Associer position -> forme
                    })
                );
                setFormesMap(formesMap);
            })
            .catch(error => console.error("Erreur lors de la récupération des données :", error))
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <p>Chargement de l'échiquier...</p>;

    return (
        <div className="chessboard-container">
            <h2>Ez Chess</h2>
            <div className="chessboard">
                {Object.keys(board).map((position, index) => {
                    const forme = formesMap.get(position); // Récupérer la forme pour chaque position
                    const type = forme ? forme.constructor.name : "vide"; // Récupérer le type de la forme

                    return (
                        <div
                            key={position}
                            className={`chess-square ${(Math.floor(index / 8) + (index % 8)) % 2 === 0 ? "light" : "dark"}`}
                        >
                            <div className={`shape ${type.toLowerCase()}`} title={type}>
                                {forme && type === "Cercle" && <div className="cercle" />}
                                {forme && type === "Carre" && <div className="carre" />}
                                {forme && type === "Triangle" && <div className="triangle" />}
                                {forme && type === "Rectangle" && <div className="rectangle" />}
                            </div>
                            <div className="form-info">
                                {forme && (
                                    <span>
                                        Aire: {forme.getAire().toFixed(2)} | Périmètre: {forme.getPerimetre().toFixed(2)}
                                    </span>
                                )}
                            </div>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}

export default ChessBoard;
