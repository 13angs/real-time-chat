import ChatPage from './pages/chat/chat.page';
import RegisterPage from './pages/register/register.page';
import LoginPage from './pages/login/login.page';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Cookies from 'js-cookie';

const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPage/>,
  },
  {
    path: "/register",
    element: <RegisterPage/>,
  },
  {
    path: "/",
    element: <ChatPage/>,
  }
]);

function App() {
  console.log(Cookies.get('login-user-id'))
  return (
    <div>
      <RouterProvider router={router} />
    </div>
  );
}

export default App;
