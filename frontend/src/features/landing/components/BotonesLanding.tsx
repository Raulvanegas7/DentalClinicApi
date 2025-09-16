import React, { useState } from "react";
import Link from "next/link";
import { ModalLanding } from "./ModalLanding";

export const BotonesLanding: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <>
      <div className="flex absolute top-120 left-26 z-10 w-[390px] justify-between">
        {/* Botón izquierdo */}
        <div className="mt-5">
          <Link
            href="/home"
            className="text-[#169EDD] text-2xl pt-5 pr-7 pb-5 pl-7 bg-white rounded-4xl font-bold shadow-2xl 
                       transition-all duration-300 ease-in-out
                       hover:bg-gray-100 hover:text-[#0a7db3] hover:shadow-[0_6px_20px_rgba(22,158,221,0.3)]
                       hover:ring-2 hover:ring-[#169EDD] hover:ring-offset-2"
          >
            Saber más
          </Link>
        </div>

        {/* Botón derecho */}
        <button
          onClick={() => setIsOpen(true)}
          className="mb-20 text-white text-2xl pt-5 pr-7 pb-5 pl-7 bg-[#169EDD] rounded-4xl font-bold shadow-2xl 
                     transition-all duration-300 ease-in-out
                     hover:bg-[#0a7db3] hover:text-gray-200 hover:shadow-[0_6px_25px_rgba(22,158,221,0.45)]
                     hover:scale-105"
        >
          Sacar turno
        </button>
      </div>

      <ModalLanding isVisible={isOpen} onClose={() => setIsOpen(false)} />
    </>
  );
};
