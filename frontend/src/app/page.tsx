"use client";

import React, { useState } from "react";
import DefinitiveLanding from "@/features/landing/components/DefinitiveLanding";
import ToothLoader from "@/features/landing/components/LoaderTooth";

export default function Home() {
  const [isLoading, setIsLoading] = useState(true);

  // No necesitamos el estado 'isReady' ya que 'DefinitiveLanding' se mostrará cuando 'isLoading' sea falso.

  const handleLoaderFinish = () => {
    // Al finalizar, simplemente ocultamos el loader
    setIsLoading(false);
  };

  return (
    <div className="relative">
      {/* El loader se muestra solo si isLoading es true */}
      {isLoading ? (
        <div className="fixed inset-0 z-50">
          <ToothLoader onFinish={handleLoaderFinish} />
        </div>
      ) : (
        // El contenido principal se muestra cuando isLoading es false
        // Con una transición de opacidad para un efecto de "fade-in"
        <div className="transition-opacity duration-1000 ease-in-out opacity-100">
          <DefinitiveLanding />
        </div>
      )}
    </div>
  );
}