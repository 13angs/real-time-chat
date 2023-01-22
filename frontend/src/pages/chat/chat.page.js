import React, { useState } from 'react';
import './chat.page.css';
import {HubConnectionBuilder, HttpTransportType} from '@microsoft/signalr';

const ChatPage = () => {
  const [messages, setMessages] = useState([
    // {id: 1, text: 'Hello', user: 'John Doe', avatar: 'https://via.placeholder.com/50x50'},
    // {id: 2, text: 'Hi', user: 'Jane Smith', avatar: 'https://via.placeholder.com/50x50'},
  ]);
  const [currentMessage, setCurrentMessage] = useState('');
  const [connection, setConnection] = React.useState(null);

  const [users, setUsers] = useState([
    {id: 1, name: 'John Doe', avatar: 'https://via.placeholder.com/50x50'},
    {id: 2, name: 'Jane Smith', avatar: 'https://via.placeholder.com/50x50'},
    {id: 3, name: 'Bob', avatar: 'https://via.placeholder.com/50x50'},
  ]);

  React.useEffect(() => {
    const chatUrl = process.env.REACT_APP_HUB_URL;
    const newConnection = new HubConnectionBuilder()
      .withUrl(chatUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
    
      newConnection.start().catch(err => {
        console.log(err);
      });
      setConnection(newConnection);

      return () => {
        newConnection.stop();
      }

  }, [])

  const handleSubmit = (event) => {
    event.preventDefault();
    setMessages([...messages, {id: Date.now(), text: currentMessage, user: 'Your Name', avatar: 'https://via.placeholder.com/50x50'}]);
    connection.invoke("SendMessage", "client", "hello world");
    setCurrentMessage('');
  }

  return (
    <div className="chat-container">
      <div className="user-list-container">
        <h3>Users</h3>
        <div className="user-list">
          {users.map((user) => (
            <div key={user.id} className="user">
              <img src={user.avatar} alt={user.name} className="avatar" />
              <div className="user-name">
                <p>{user.name}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
      <div className="chat-messages-container">
        <div className="chat-header">
          <h2>Chat Room</h2>
        </div>
        <div className="chat-messages">
          {messages.map((message) => (
            <div key={message.id} className="message">
              <img src={message.avatar} alt={message.user} className="avatar" />
              <div className="message-text">
                <p><strong>{message.user}:</strong> {message.text}</p>
              </div>
            </div>
          ))}
        </div>
        <form className="chat-form" onSubmit={handleSubmit}>
          <input 
            className="chat-input"
            type="text" 
            placeholder="Type a message"
            value={currentMessage} 
            onChange={(event) => setCurrentMessage(event.target.value)}
          />
          <button className="chat-submit" type="submit">Send</button>
        </form>
      </div>
    </div>
  );
}

export default ChatPage;
