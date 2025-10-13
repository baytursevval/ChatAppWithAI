import React, { useState, useEffect } from "react";
import axios from "axios";
import "./Login.css";

function Login({ onLogin }) {
  const [nickname, setNickname] = useState("");
  const [users, setUsers] = useState([]);
  const [infoMessage, setInfoMessage] = useState("");

  useEffect(() => {
    axios.get("https://chatemotionapi.onrender.com/api/user")
      .then(res => setUsers(res.data))
      .catch(err => console.error(err));
  }, []);

  const handleLogin = () => {
    const existingUser = users.find(u => u.nickname === nickname);
    if (existingUser) {
      onLogin(existingUser);
      setInfoMessage("");
    } else {
      axios.post("https://chatemotionapi.onrender.com/api/user/register", { nickname })
        .then(res => {
          onLogin(res.data);
          setInfoMessage(`Yeni kullanıcı "${nickname}" kaydedildi!`);
        })
        .catch(err => console.error(err));
    }
  };

  return (
    <div className="login-container">
      <div className="login-box">
        <h2>Rumuz ile giriş</h2>
        <input
          type="text"
          value={nickname}
          onChange={(e) => setNickname(e.target.value)}
          placeholder="Rumuz"
        />
        <button onClick={handleLogin}>Giriş</button>
        
        {infoMessage && <p className="info-message">{infoMessage}</p>}
      </div>
    </div>
  );
}

export default Login;
