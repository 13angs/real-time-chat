import React, { useState } from 'react';
import './chat.page.css';
import Axios from '../../backend/Axios';
import { HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr';

const ChatPage = () => {
  const [messages, setMessages] = useState([
    // {id: 1, text: 'Hello', user: 'John Doe', avatar: 'https://via.placeholder.com/50x50'},
    // {id: 2, text: 'Hi', user: 'Jane Smith', avatar: 'https://via.placeholder.com/50x50'},
  ]);
  const [currentMessage, setCurrentMessage] = useState('');
  const [connection, setConnection] = React.useState(null);

  const [users, setUsers] = useState([]);

  const apiUrl = process.env.REACT_APP_API_URL;
  const messUrl = `${apiUrl}/api/v1/messages`;
  const chatUrl = `${apiUrl}/hub/chat`;

  React.useEffect(() => {
    const fetchMessage = async (url) => {
      try {
        const res = await Axios.get(url);
        if (res.status === 200) {
          const data = res.data;
          setMessages(data);
        }
      } catch (err) {
        console.log(err);
      }
    }

    const fetchUsers = async (url) => {
      try {
        const res = await Axios.get(url);
        if (res.status === 200) {
          const data = res.data;
          setUsers(data);
        }
      } catch (err) {
        console.log(err);
      }
    }

    fetchMessage('/api/v1/messages');
    fetchUsers('/api/v1/users');
  }, [messUrl])

  React.useEffect(() => {
    const fetchMessage = async (url) => {
      try {
        const res = await Axios.get(url);
        if (res.status === 200) {
          const data = res.data;
          setMessages(data);
        }
      } catch (err) {
        console.log(err);
      }
    }

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
    newConnection.on("ReceiveMessage", async () => {
      fetchMessage('/api/v1/messages');
    });

    return () => {
      newConnection.stop();
    }

  }, [messUrl, chatUrl])

  const handleSubmit = (event) => {
    event.preventDefault();
    connection.invoke("SendMessage", "client", currentMessage);
    setCurrentMessage('');
  }

  return (
    <div className="chat-container">
      <div className="user-list-container">
        <h3>Users</h3>
        <div className="user-list">
          {users.length > 0 && users.map((user) => (
            <div key={user.id} className="user">
              <img src={'https://via.placeholder.com/50x50'} alt={user.name} className="avatar" />
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
              <img src={'https://via.placeholder.com/50x50'} alt={'https://via.placeholder.com/50x50'} className="avatar" />
              <div className="message-text">
                <p><strong>{message.from}:</strong> {message.text}</p>
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
