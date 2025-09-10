"use client"

import React from "react"
import { useState } from "react";

export const ModalLanding: React.FC = ({isVisible, onClose}) => {


    if(!isVisible) return null;
    
    const handleClose = (e) => {

        if(e.target.id === "wrapper") onClose()
    }

    // formulario para sacar turno por whatsapp
    const [formData, setFormData] = useState({
    nombre: "",
    apellido: "",
    edad: "",
    area:"",
    telefono: "",
    motivo: "",
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    // Número de WhatsApp de la secretaria (formato internacional, sin + ni espacios)
    const secretariaNumber = "543515067576";

    // Crear mensaje predefinido con datos del usuario
    const mensaje = `Hola, mi nombre es ${formData.nombre} ${formData.apellido}, tengo ${formData.edad} años. Quisiera sacar un turno con el odontólogo especialista en ${formData.area}. el motivo de la consulta es: ${formData.motivo}. Mi número de contacto: ${formData.telefono}`;

    // URL de WhatsApp Web para enviar mensaje
    const url = `https://wa.me/${secretariaNumber}?text=${encodeURIComponent(mensaje)}`;

    // Abrir WhatsApp Web en nueva pestaña con el mensaje
    window.open(url, "_blank");
  };


    return(
            <div id="wrapper" onClick={handleClose} className="fixed inset-0 bg-black/50 z-40 backdrop-blur-xs flex justify-center items-start pt-30 ">
                <div className="w-[600px] flex flex-col">
                    <button onClick={() => onClose()} className="text-white m-2 bg-red-600 px-3 py-1 rounded-full text-xl place-self-end">X</button>
                    <div className="bg-white p-2 h-[450px] rounded-2xl text-black text-center z-50 ">
                        <form onSubmit={handleSubmit} className="flex flex-col gap-8 p-4 m-auto">
                            <input type="text" name="nombre" placeholder="Nombre" value={formData.nombre} onChange={handleChange} required />
                            <input type="text" name="apellido" placeholder="Apellido" value={formData.apellido} onChange={handleChange} required />
                            <input type="number" name="edad" placeholder="Edad" value={formData.edad} onChange={handleChange} required />
                            <select name="area" value={formData.area} onChange={handleChange} className="text-gray-500 p-0 mr-2" required>
                            <option value="">Seleccioná un área (opcional)</option>
                            <option value="Endodoncia">Endodoncia</option>
                            <option value="Odontopediatría">Odontopediatría</option>
                            <option value="Ortodoncia">Ortodoncia</option>
                            <option value="Periodoncia">Periodoncia</option>
                            <option value="Cirugía Oral">Cirugía Oral</option>
                            <option value="Prótesis">Prótesis</option>
                            <option value="Otra">Otra</option>
                            </select>

                            <input type="tel" name="telefono" placeholder="Número de teléfono" value={formData.telefono} onChange={handleChange} required />
                            <textarea name="motivo" placeholder="Motivo de la consulta" value={formData.motivo} onChange={handleChange} required />
                            <button type="submit" className="bg-[#169EDD] text-white p-2 rounded">Enviar WhatsApp</button>
    </form>
                    </div>
                </div>
            </div>
    )
}