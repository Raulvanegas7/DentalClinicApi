import { useEffect, useState } from 'react';
import api from '../api/api';

export default function Appointments() {
  const [appointments, setAppointments] = useState([]);

  useEffect(() => {
    api.get('/appointment')
      .then(res => setAppointments(res.data))
      .catch(err => console.error(err));
  }, []);

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Citas</h1>
      <ul>
        {appointments.map((a: any) => (
          <li key={a.id} className="border p-2 mb-2 rounded">
            <p><strong>Paciente:</strong> {a.patientId}</p>
            <p><strong>Fecha:</strong> {a.date}</p>
            <p><strong>Status:</strong> {a.status}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}
