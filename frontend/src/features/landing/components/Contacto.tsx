import React from "react";
import Image from "next/image";
import { Home, Mail, Phone } from "lucide-react";

const Contacto: React.FC = () => {
  return (
    <div id="contacto" className="w-[1170px] m-auto mt-20 mb-11">
      {/* Sección superior con imagen de fondo */}
      <div className="relative w-full h-[600px]">
        <Image
          src="/contactoImagen.png"
          alt="contacto dental clinic"
          fill
          className="object-cover object-[10%_18%] "
          
        />
        <div className="absolute inset-0 bg-black/15 flex flex-col justify-center items-center text-center px-4">
          <h2 className="text-white text-5xl font-semibold">
            contactate con{" "}
            <span className="text-[#169EDD] text-5xl font-bold">nosotros ahora</span>
          </h2>
          <p className="text-white text-2xl mt-2 max-w-2xl">
            puedes contactarte con nuestro equipo técnico a través de...
          </p>
        </div>
      </div>

      {/* Sección de tarjetas */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6 max-w-[1200px] mx-auto px-6 -mt-20 relative z-20">
        {/* Dirección */}
        <div className="bg-white rounded-2xl shadow-lg p-8 text-center 
                        transition-all duration-300 
                        hover:scale-105 hover:shadow-2xl 
                        hover:ring-2 hover:ring-[#169EDD] hover:ring-offset-2">
          <Home className="w-14 h-14 text-[#169EDD] mx-auto mb-5" />
          <h3 className="text-2xl font-bold text-[#169EDD] mb-3">Dirección</h3>
          <p className="text-[#169EDD] text-lg mb-3">
            Av. Libertad 1450, Piso 2 – Consultorio 203 – CABA
          </p>
          <p className="text-[#169EDD] text-md">
            Nos encontramos en una <strong>ubicación de fácil acceso</strong>, con
            transporte público cercano y ascensor disponible. Atendemos con turno
            previo en un ambiente moderno, cómodo y seguro.
          </p>
        </div>

        {/* Correo electrónico */}
        <div className="bg-white rounded-2xl shadow-lg p-8 text-center 
                        transition-all duration-300 
                        hover:scale-105 hover:shadow-2xl 
                        hover:ring-2 hover:ring-[#169EDD] hover:ring-offset-2">
          <Mail className="w-14 h-14 text-[#169EDD] mx-auto mb-5" />
          <h3 className="text-2xl font-bold text-[#169EDD] mb-3">
            Correo electrónico
          </h3>
          <p className="text-[#169EDD] text-lg font-medium mb-3">
            DentalClinic@gmail.com
          </p>
          <p className="text-[#169EDD] text-base">
            Para consultas, turnos, presupuestos o derivaciones, podés
            escribirnos por correo electrónico. Respondemos dentro de las 24
            horas hábiles con toda la información que necesites.
          </p>
        </div>

        {/* Teléfono y WhatsApp */}
        <div className="bg-white rounded-2xl shadow-lg p-8 text-center 
                        transition-all duration-300 
                        hover:scale-105 hover:shadow-2xl 
                        hover:ring-2 hover:ring-[#169EDD] hover:ring-offset-2">
          <Phone className="w-14 h-14 text-[#169EDD] mx-auto mb-5" />
          <h3 className="text-2xl font-bold text-[#169EDD] mb-3">
            Teléfono y WhatsApp
          </h3>
          <p className="text-[#169EDD] text-lg font-medium mb-3">
            +54-9-113-456-7890
          </p>
          <p className="text-[#169EDD] text-base">
            Comunicate por llamada o WhatsApp para coordinar turnos, confirmar
            horarios o hacer consultas rápidas. Nuestro equipo de atención está
            disponible de lunes a viernes de 9 a 18 h.
          </p>
        </div>
      </div>
    </div>
  );
};

export default Contacto;
