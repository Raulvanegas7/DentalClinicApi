import { useState } from 'react';
import api from '../api/api';

export default function Signup() {
  const [form, setForm] = useState({ username: '', email: '', password: '' });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await api.post('/user/signup', form);
      localStorage.setItem('token', res.data.token);
      alert('Registrado correctamente!');
    } catch (error) {
      console.error(error);
      alert('Error al registrar');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto mt-10 p-4 border rounded">
      <input name="username" placeholder="Username" value={form.username} onChange={handleChange} required className="block w-full mb-4 p-2 border"/>
      <input name="email" placeholder="Email" value={form.email} onChange={handleChange} required className="block w-full mb-4 p-2 border"/>
      <input type="password" name="password" placeholder="Password" value={form.password} onChange={handleChange} required className="block w-full mb-4 p-2 border"/>
      <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">Registrar</button>
    </form>
  );
}
