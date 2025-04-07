// src/App.tsx
import React from "react";
import "./App.css";
import ChessBoard from "./ChessBoard";

const App: React.FC = () => {
  return (
    <div className="App">
      <h1>Jeu d'Échecs</h1>
      <ChessBoard />
    </div>
  );
};

export default App;