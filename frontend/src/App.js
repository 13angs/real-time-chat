import ChatPage from './pages/chat/chat.page';
import RegisterPage from './pages/register/register.page';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';

const router = createBrowserRouter([
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
  return (
    <div>
      <RouterProvider router={router} />
    </div>
  );
}

export default App;
