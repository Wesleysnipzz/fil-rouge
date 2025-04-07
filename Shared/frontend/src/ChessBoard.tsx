// src/ChessBoard.tsx
import React, { useEffect, useState } from "react";
import { Cercle, Carre, Rectangle, Triangle } from './GeometricShapes';
import { FormeBase, Board } from './types/shapes';
import { getBoard, getForms } from './services/api';
import "./ChessBoard.css";

type FormesMap = Map<string, FormeBase | null>;

function ChessBoard() {
    const [board, setBoard] = useState<Board | null>(null);
    const [formesMap, setFormesMap] = useState<FormesMap>(new Map());
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        setLoading(true);

        Promise.all([getBoard(), getForms()])
            .then(([boardData, formesData]) => {
                setBoard(boardData);

                const formesMap = new Map(
                    formesData.map(f => {
                        let forme: FormeBase | null = null;
                        switch (f.type?.toLowerCase()) {
                            case "cercle":
                                forme = new Cercle(f.rayon, f.position, f.boardId);
                                break;
                            case "carre":
                                forme = new Carre(f.cote, f.position, f.boardId);
                                break;
                            case "rectangle":
                                forme = new Rectangle(f.longueur, f.largeur, f.position, f.boardId);
                                break;
                            case "triangle":
                                forme = new Triangle(f.base, f.hauteur, f.position, f.boardId);
                                break;
                        }
                        return [f.position, forme];
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
                {board && Object.keys(board).map((position, index) => {
                    const forme = formesMap.get(position);
                    const type = forme ? forme.constructor.name : "vide";

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