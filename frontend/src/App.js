import React, { useState, useEffect } from "react";
import Login from "./components/Login";
import Chat from "./components/Chat";

function App() {
  const [user, setUser] = useState(null);

  return (
    <div className="App">
      {!user ? (
        <Login onLogin={setUser} />
      ) : (
        <Chat currentUser={user} />
      )}
    </div>
  );
}

export default App;
