import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import CreateAppointment from './pages/CreateAppointment'
import Login from './pages/Login'
import Signup from './pages/Signup'
import Home from './pages/Home'

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/signup" element={<Signup />} />
        <Route path="/login" element={<Login />} />
        <Route path="/appointments/new" element={<CreateAppointment />} />
      </Routes>
    </Router>
  )
}

export default App
