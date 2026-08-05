import { Link, Outlet } from 'react-router-dom'

function RootLayout() {
  return (
    <>
      <nav aria-label="Navegação principal">
        <Link to="/">Início</Link>
        <Link to="/products">Produtos</Link>
        <Link to="/about">Sobre</Link>
      </nav>
      <Outlet />
    </>
  )
}

export default RootLayout
