import './App.css'
import { useState } from 'react'


type Product = {
  name: string
  basePrice: number
}

const products: Product[] = [
  { name: 'MSI RTX 5060 Ti Ventus 2X OC Plus', basePrice: 2699.99 },
  { name: 'MSI GeForce RTX 5070 12G VENTUS 2X OC', basePrice: 4399.99 },
  { name: 'ASRock RX 9060 XT CL 16GB AMD Radeon', basePrice: 2799.99 },
  { name: 'ASRock RX 9070 XT Challenger AMD 16GB', basePrice: 4299.99 },
  { name: 'Asus TUF-RTX 5070 TI 16G GAMING 16GB', basePrice: 9226 },
]

const formatter = new Intl.NumberFormat('pt-BR', {
  style: 'currency',
  currency: 'BRL',
})

function randomVariation(minPercent: number, maxPercent: number) {
  return Math.random() * (maxPercent - minPercent) + minPercent
}

function applyRandomPrice(basePrice: number) {
  const variation = randomVariation(-0.07, 0.07)
  return basePrice * (1 + variation)
}

function App() {
  const [productsWithPrice] = useState(() =>
    products.map((product) => ({
      ...product,
      price: applyRandomPrice(product.basePrice),
    })),
  )

  return (
    <main className="container">
      <h1>Lista de Produtos</h1>
      <p>Preços com variação aleatória entre -7% e +7% sobre o valor base.</p>
      <ul id="products-list">
        {productsWithPrice.map((product) => (
          <li key={product.name}>
            <span className="name">{product.name}</span>
            <span className="price">{formatter.format(product.price)}</span>
          </li>
        ))}
      </ul>
    </main>
  )
}

export default App
