import React, { useState } from "react";
import Image from "next/image";
import { ModalLanding } from "./ModalLanding";

const CallToAction: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <>
      <div className="relative w-full h-[600px] mt-28 mb-28">
        {/* Imagen de fondo */}
        <Image
          src="/CTAimagen.png"
          alt="Hero Dental Clinic"
          fill
          className="object-contain object-[10%_15%]"
          objectPosition="center"
          priority
        />

        {/* Overlay oscuro */}
        <div className="absolute inset-0 bg-black/30 flex flex-col justify-center items-center text-center px-4">
          {/* Texto */}
          <h2 className="text-white text-2xl md:text-4xl font-semibold w-[1200px] mt-25 leading-snug">
            Una sonrisa saludable abre puertas. Permítanos ayudarlo a cuidar la suya y agende su turno hoy.
          </h2>

          {/* Botón estilo moderno */}
          <button
            onClick={() => setIsOpen(true)}
            className="
              mt-6 px-8 py-4 text-lg md:text-2xl font-semibold text-white
    bg-blue-600 rounded-xl
    transition transform duration-300 ease-in-out
    hover:bg-blue-500 hover:scale-105
    focus:outline-none focus:ring-4 focus:ring-blue-300/40
            "
          >
            Agendar mi turno
          </button>
        </div>
      </div>

      <ModalLanding isVisible={isOpen} onClose={() => setIsOpen(false)} />
    </>
  );
};

export default CallToAction;
