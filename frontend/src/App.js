import ChatPage from './pages/chat/chat.page';
import RegisterPage from './pages/register/register.page';
import LoginPage from './pages/login/login.page';
import Protected from './routes/ProtectedRoute';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Cookies from 'js-cookie';
import routes from './routes/routes';

const userId = Cookies.get('login-user-id');

const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
  {
    path: routes.chat.path,
    element:
      <Protected isSignedIn={userId}>
        <ChatPage userId={userId} />
      </Protected>
  },
  {
    path: "/",
    element:
      <Protected isSignedIn={userId}>
        <div>
          <h1>You already logged in</h1>
        </div>
      </Protected>
  },
]);

function App() {

  const handleLogout = () => {
    Cookies.remove('login-user-id');
    window.location.reload();
  }
  return (
    <div>
      <RouterProvider router={router} />
      {userId &&
        <button onClick={handleLogout}>Logout</button>
      }
    </div>
  );
}

export default App;
