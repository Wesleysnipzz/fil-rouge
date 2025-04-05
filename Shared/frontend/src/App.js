import React from "react";
import "./App.css"; // ✅ Import correct du CSS
import ChessBoard from "./ChessBoard";

function App() {
    return (
        <div className="App">
            <h1>Jeu d'Échecs</h1>  {/* Titre de l'application */}
            <ChessBoard />  {/* Affichage de l'échiquier */}
        </div>
    );
}

export default App;
