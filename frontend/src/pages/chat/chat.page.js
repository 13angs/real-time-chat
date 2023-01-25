import React, { useState } from 'react';
import './chat.page.css';
import Axios from '../../backend/Axios';
import { UserAPI } from '../../backend/api';
import { HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr';
import { useParams, useNavigate, useSearchParams } from 'react-router-dom';
import routes from '../../routes/routes';


// get and display the user list
function UserList({from}) {
  const [users, setUsers] = useState([]);
  const navigate = useNavigate();

  React.useEffect(() => {
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

    fetchUsers('/api/v1/users');
  }, [])

  // when click the user push param as to=<user_id>
  const handleUserClick = (userId) => {
    navigate({
      pathname: routes.chat.path.replace(':id', from), 
      search: `?${routes.chat.search.to}=${userId}`});
  }

  return (
    <div className="user-list-container">
      <h3>Users</h3>
      <div className="user-list">
        {users.length > 0 && users.map((user) => (
          <div key={user.id} className="user" onClick={() => handleUserClick(user.id)}>
            <img src={'https://via.placeholder.com/50x50'} alt={user.name} className="avatar" />
            <div className="user-name">
              <p>{user.name}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

// get and display the chat messages
function ChatMessage({userId}) {
  const searchParams = useSearchParams();
  const allParams = searchParams[0];
  const [messages, setMessages] = useState([]);
  const [currentUser, setCurrentUser] = useState(null);

  const [currentMessage, setCurrentMessage] = useState('');
  const [connection, setConnection] = React.useState(null);

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

    const fetchUser = () => {
      const user = new UserAPI();
      user.getUser(userId, setCurrentUser)
    }

    fetchMessage('/api/v1/messages');
    fetchUser();
  }, [messUrl, userId])

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

  return (<div className="chat-messages-container">
    <div className="chat-header">
      <h2>Login as {currentUser ? currentUser['name'] : "Guest"}</h2>
    </div>
    <div className="chat-messages">
      {allParams.get('to') && messages.map((message) => (
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
  </div>)
}

const ChatPage = () => {
  const { id } = useParams();
  return (
    <div className="chat-container">
      <UserList from={id} />
      <ChatMessage userId={id}/>
    </div>
  );
}

export default ChatPage;
