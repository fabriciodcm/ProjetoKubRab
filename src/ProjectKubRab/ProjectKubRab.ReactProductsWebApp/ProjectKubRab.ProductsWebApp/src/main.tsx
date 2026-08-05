import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import './index.css'
import App from './App.tsx'
import RootLayout from './RootLayout.tsx'

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    children: [
      {
        index: true,
        element: <main className="container"><h1>Página inicial</h1></main>,
      },
      {
        path: "products",
        element: <App />,
      },
      {
        path: "about",
        element: <main className="container"><h1>Sobre</h1><p>Aplicação de acompanhamento de preços de produtos.</p></main>,
      },
    ],
  },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
