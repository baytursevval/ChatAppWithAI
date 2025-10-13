import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import "./Chat.css";

function Chat({ currentUser }) {
  const [messages, setMessages] = useState([]);
  const [text, setText] = useState("");
  const [users, setUsers] = useState([]);
  const [receiverId, setReceiverId] = useState("");
  const messagesEndRef = useRef(null);

  useEffect(() => {
    axios.get("https://chatemotionapi.onrender.com/api/user")
      .then(res => setUsers(res.data.filter(u => u.id !== currentUser.id)))
      .catch(err => console.error(err));

    fetchMessages();
  }, []);

  const fetchMessages = () => {
    if (!receiverId) return;

    axios.get(`https://chatemotionapi.onrender.com/api/message?user1=${currentUser.id}&user2=${receiverId}`)
      .then(res => setMessages(res.data))
      .catch(err => console.error(err));
  };

  const handleSend = () => {
    if (!text || !receiverId) return;

    const payload = {
      text,
      senderId: currentUser.id,
      receiverId: parseInt(receiverId)
    };

    axios.post("https://chatemotionapi.onrender.com/api/message", payload)
      .then(res => {
        setText("");
        fetchMessages();
      })
      .catch(err => console.error(err));
  };

  useEffect(() => {
    fetchMessages();
    const interval = setInterval(fetchMessages, 3000);
    return () => clearInterval(interval);
  }, [receiverId]);

  useEffect(() => {
    // Scroll to bottom when messages update
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  return (
    <div className="chat-container">
      <h3>Hoşgeldin, {currentUser.nickname}</h3>

      <select 
        value={receiverId} 
        onChange={e => setReceiverId(e.target.value)}
        className="user-select"
      >
        <option value="">Mesaj gönderecek kullanıcıyı seç</option>
        {users.map(u => (
          <option key={u.id} value={u.id}>{u.nickname}</option>
        ))}
      </select>

      <div className="messages-container">
        {messages.map(m => (
          <div 
            key={m.id} 
            className={`message ${m.senderId === currentUser.id ? "sent" : "received"}`}
          >
            <div className="message-text">{m.text}</div>
            <div className="message-meta">
              {m.sentiment} ({m.score.toFixed(2)})
            </div>
          </div>
        ))}
        <div ref={messagesEndRef} />
      </div>

      <div className="send-container">
        <input
          type="text"
          value={text}
          onChange={e => setText(e.target.value)}
          placeholder="Mesaj yaz..."
        />
        <button onClick={handleSend}>Gönder</button>
      </div>
    </div>
  );
}

export default Chat;
