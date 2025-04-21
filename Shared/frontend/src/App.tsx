// src/App.tsx
import React from "react";
import "./App.css";
import ChessBoard from "./ChessBoard.js";

const App: React.FC = () => {
  return (
    <div className="App">
      <header className="App-header">
        <h1>EzChess - Affichage des Formes Géométriques</h1>
      </header>
      <main>
        <ChessBoard />
      </main>
      <footer className="App-footer">
        <p>Projet EzChess - Affichage des formes géométriques sur un échiquier</p>
      </footer>
    </div>
  );
};

export default App;