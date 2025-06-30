import { useState } from 'react'
import Navbar from './components/Navbar'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Navbar />
      <main className="p-4">
        <h1 className="text-2xl font-bold mb-4">Página de Inicio</h1>
        <button onClick={() => setCount(count + 1)} className="px-4 py-2 bg-green-500 text-white rounded">
          Count is {count}
        </button>
      </main>
    </>
  )
}

export default App
