import { useState, useEffect } from 'react'
import api from '../api/api'
import { useNavigate } from 'react-router-dom'

interface Option {
  id: string
  name: string
}

const CreateAppointment = () => {
  const navigate = useNavigate()

  // State para IDs
  const [patients, setPatients] = useState<Option[]>([])
  const [dentists, setDentists] = useState<Option[]>([])
  const [services, setServices] = useState<Option[]>([])

  // State para form
  const [form, setForm] = useState({
    patientId: '',
    dentistId: '',
    serviceId: '',
    date: '',
    notes: '',
  })

  const [error, setError] = useState('')

  // Cargar opciones
  useEffect(() => {
    const fetchData = async () => {
      try {
        const token = localStorage.getItem('token')

        const [patientsRes, dentistsRes, servicesRes] = await Promise.all([
          api.get('/patient', {
            headers: { Authorization: `Bearer ${token}` },
          }),
          api.get('/dentist', {
            headers: { Authorization: `Bearer ${token}` },
          }),
          api.get('/service', {
            headers: { Authorization: `Bearer ${token}` },
          }),
        ])

        setPatients(patientsRes.data)
        setDentists(dentistsRes.data)
        setServices(servicesRes.data)
      } catch (err) {
        console.error(err)
        setError('Error cargando opciones.')
      }
    }

    fetchData()
  }, [])

  // Cambio de inputs
  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setError('')

    try {
      const token = localStorage.getItem('token')
      await api.post('/appointment', form, {
        headers: { Authorization: `Bearer ${token}` },
      })

      alert('Cita agendada correctamente.')
      navigate('/appointments') // Redirige a lista de citas (si tienes una)
    } catch (err) {
      console.error(err)
      setError('Error al agendar la cita.')
    }
  }

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded shadow">
      <h1 className="text-2xl font-bold mb-4">Agendar Cita</h1>
      {error && <p className="text-red-500 mb-4">{error}</p>}
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <select
          name="patientId"
          value={form.patientId}
          onChange={handleChange}
          className="border p-2 rounded"
          required
        >
          <option value="">Seleccione Paciente</option>
          {patients.map((p) => (
            <option key={p.id} value={p.id}>
              {p.name}
            </option>
          ))}
        </select>

        <select
          name="dentistId"
          value={form.dentistId}
          onChange={handleChange}
          className="border p-2 rounded"
          required
        >
          <option value="">Seleccione Odontólogo</option>
          {dentists.map((d) => (
            <option key={d.id} value={d.id}>
              {d.name}
            </option>
          ))}
        </select>

        <select
          name="serviceId"
          value={form.serviceId}
          onChange={handleChange}
          className="border p-2 rounded"
          required
        >
          <option value="">Seleccione Servicio</option>
          {services.map((s) => (
            <option key={s.id} value={s.id}>
              {s.name}
            </option>
          ))}
        </select>

        <input
          type="datetime-local"
          name="date"
          value={form.date}
          onChange={handleChange}
          className="border p-2 rounded"
          required
        />

        <textarea
          name="notes"
          placeholder="Notas"
          value={form.notes}
          onChange={handleChange}
          className="border p-2 rounded"
        ></textarea>

        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          Agendar
        </button>
      </form>
    </div>
  )
}

export default CreateAppointment
