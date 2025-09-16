"use client";

import React, { useState, useEffect } from "react";

export const ModalLanding: React.FC<{ isVisible: boolean; onClose: () => void }> = ({
  isVisible,
  onClose,
}) => {
  const [show, setShow] = useState(false);
  const [isBouncing, setIsBouncing] = useState(false);

  const [formData, setFormData] = useState({
    nombre: "",
    apellido: "",
    edad: "",
    area: "",
    motivo: "",
  });

  useEffect(() => {
    if (isVisible) {
      setShow(true);
      setTimeout(() => setIsBouncing(true), 10);
    } else {
      setIsBouncing(false);
    }
  }, [isVisible]);

  const handleClose = (e: React.MouseEvent<HTMLDivElement>) => {
    if ((e.target as HTMLElement).id === "wrapper") {
      setShow(false);
      setTimeout(() => onClose(), 300);
    }
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const secretariaNumber = "543515067576";
    const mensaje = `Hola, mi nombre es ${formData.nombre} ${formData.apellido}, tengo ${formData.edad} años y Quisiera sacar un turno con el odontólogo especialista en ${formData.area}. El motivo de la consulta es: ${formData.motivo}.`;
    const url = `https://wa.me/${secretariaNumber}?text=${encodeURIComponent(mensaje)}`;
    window.open(url, "_blank");
  };

  if (!isVisible && !show) return null;

  return (
    <div
      id="wrapper"
      onClick={handleClose}
      className={`fixed inset-0 bg-black/50 z-40 backdrop-blur-sm flex justify-center items-center transition-opacity duration-300 ${
        show ? "opacity-100" : "opacity-0"
      }`}
    >
      <div
        className={`w-[600px] max-h-[88vh] bg-white rounded-2xl mt-20 shadow-lg overflow-hidden flex flex-col transform transition-all duration-500 ${
          show
            ? isBouncing
              ? "translate-y-0 scale-100 hover:scale-[1.01] hover:shadow-2xl"
              : "translate-y-24 scale-90"
            : "-translate-y-20"
        }`}
      >
        {/* Header */}
        <div className="flex justify-center items-center px-6 pt-4">
          <h2 className="text-[#169EDD] font-bold text-3xl mt-2 text-center">Agendá tu turno</h2>
        </div>

        {/* Contenido con scroll interno */}
        <div className="overflow-y-auto px-6 py-4">
          <p className="text-gray-600 mb-2">
            Completá el formulario y una recepcionista se pondrá en contacto con vos por WhatsApp
            para confirmar día y horario.
          </p>
          <p className="text-[#169EDD] text-sm mb-4">
            Los campos marcados con <span className="text-red-500 text-xl">*</span> son obligatorios.
          </p>

          <form onSubmit={handleSubmit} className="flex flex-col gap-3">
            {/* Nombre */}
            <div className="flex flex-col">
              <label htmlFor="nombre" className="text-sm font-medium text-gray-700">
                Nombre <span className="text-red-500">*</span>
              </label>
              <input
                type="text"
                id="nombre"
                name="nombre"
                placeholder="Ingresá tu nombre"
                value={formData.nombre}
                onChange={handleChange}
                required
                className="mt-1 p-2 border border-[#169EDD] rounded-md text-black 
                           focus:outline-none focus:ring-2 focus:ring-[#169EDD] 
                           hover:border-[#1286ba] transition"
              />
            </div>

            {/* Apellido */}
            <div className="flex flex-col">
              <label htmlFor="apellido" className="text-sm font-medium text-gray-700">
                Apellido <span className="text-red-500">*</span>
              </label>
              <input
                type="text"
                id="apellido"
                name="apellido"
                placeholder="Ingresá tu apellido"
                value={formData.apellido}
                onChange={handleChange}
                required
                className="mt-1 p-2 border border-[#169EDD] rounded-md text-black 
                           focus:outline-none focus:ring-2 focus:ring-[#169EDD] 
                           hover:border-[#1286ba] transition"
              />
            </div>

            {/* Edad */}
            <div className="flex flex-col">
              <label htmlFor="edad" className="text-sm font-medium text-gray-700">
                Edad <span className="text-red-500">*</span>
              </label>
              <input
                type="number"
                name="edad"
                id="edad"
                placeholder="Ingresá tu edad"
                value={formData.edad}
                onChange={handleChange}
                required
                className="mt-1 p-2 border border-[#169EDD] rounded-md text-black 
                           focus:outline-none focus:ring-2 focus:ring-[#169EDD] 
                           hover:border-[#1286ba] transition"
              />
            </div>

            {/* Área */}
            <div className="flex flex-col">
              <label htmlFor="area" className="text-sm font-medium text-gray-700">
                Área de atención (opcional)
              </label>
              <select
                name="area"
                id="area"
                value={formData.area}
                onChange={handleChange}
                className="mt-1 p-2 border border-[#169EDD] rounded-md text-gray-600 
                           focus:outline-none focus:ring-2 focus:ring-[#169EDD] 
                           hover:border-[#1286ba] transition"
              >
                <option value="">Seleccioná un área</option>
                <option value="Endodoncia">Endodoncia</option>
                <option value="Odontopediatría">Odontopediatría</option>
                <option value="Ortodoncia">Ortodoncia</option>
                <option value="Periodoncia">Periodoncia</option>
                <option value="Cirugía Oral">Cirugía Oral</option>
                <option value="Prótesis">Prótesis</option>
                <option value="Otra">Otra</option>
              </select>
            </div>

            {/* Motivo */}
            <div className="flex flex-col">
              <label htmlFor="motivo" className="text-sm font-medium text-gray-700">
                Motivo de la consulta <span className="text-red-500">*</span>
              </label>
              <textarea
                name="motivo"
                id="motivo"
                placeholder="Contanos brevemente tu consulta"
                value={formData.motivo}
                onChange={handleChange}
                required
                className="mt-1 p-2 border border-[#169EDD] rounded-md text-black 
                           focus:outline-none focus:ring-2 focus:ring-[#169EDD] 
                           hover:border-[#1286ba] transition"
                rows={2}
              />
            </div>

            {/* Botón */}
            <button
              type="submit"
              className="w-full bg-[#169EDD] text-white font-semibold py-2 px-4 rounded-md 
                         transition-all duration-300 ease-in-out
                         hover:bg-[#1286ba] hover:shadow-lg hover:scale-[1.02] mt-2"
            >
              Enviar WhatsApp
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};
