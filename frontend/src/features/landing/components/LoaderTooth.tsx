"use client";

import React, { useState, useEffect } from "react";
import { Tooth } from "healthicons-react";
import { datosOdontologia } from "./arregloDatosCuriosos";

interface ToothLoaderProps {
  onFinish: () => void;
}

const ToothLoader: React.FC<ToothLoaderProps> = ({ onFinish }) => {
  const [progress, setProgress] = useState(0);
  const [waveOffset, setWaveOffset] = useState(0);
  const [datoCurioso, setDatoCurioso] = useState("");
  const [isEntering, setIsEntering] = useState(true);
  const [isTransitioningOut, setIsTransitioningOut] = useState(false);

  useEffect(() => {
    // ⬅️ This useEffect controls the entry animation
    setTimeout(() => {
      setIsEntering(false);
    }, 500); // Increased delay to 500ms
  }, []);

  useEffect(() => {
    const randomIndex = Math.floor(Math.random() * datosOdontologia.length);
    setDatoCurioso(datosOdontologia[randomIndex]);
  }, []);

  useEffect(() => {
    if (progress < 100) {
      const interval = setInterval(() => {
        setProgress((prev) => prev + 1);
      }, 30);
      return () => clearInterval(interval);
    } else {
      setIsTransitioningOut(true);
      setTimeout(() => {
        onFinish();
      }, 800);
    }
  }, [progress, onFinish]);

  useEffect(() => {
    const waveInterval = setInterval(() => {
      setWaveOffset((prev) => prev + 0.1);
    }, 30);
    return () => clearInterval(waveInterval);
  }, []);

  const wavePath = () => {
    const amplitude = 1.5;
    const frequency = 0.2;
    const width = 24;
    const height = 24;
    const fillHeight = (progress / 100) * height;

    let pathData = `M0,${height}`;
    for (let x = 0; x <= width; x++) {
      const y = height - fillHeight + amplitude * Math.sin(x * frequency - waveOffset);
      pathData += ` L${x},${y}`;
    }
    pathData += ` L${width},${height} Z`;

    return pathData;
  };

  return (
    <div className={`flex flex-col justify-center items-center h-screen bg-white transition-transform duration-700 ease-in-out ${isEntering ? '-translate-y-full' : 'translate-y-0'} ${isTransitioningOut ? '-translate-y-full' : ''}`}>
      <div className="relative w-56 h-56 mx-auto">
        <Tooth
          className="absolute top-0 left-0 w-full h-full text-gray-300"
          strokeWidth={1.5}
        />
        <svg
          viewBox="0 0 24 24"
          className="absolute top-0 left-0 w-full h-full"
          fill="none"
        >
          <mask id="tooth-mask">
            <Tooth className="w-full h-full text-white" strokeWidth={1.5} />
          </mask>
          <path d={wavePath()} fill="#23aeef" mask="url(#tooth-mask)" />
        </svg>
      </div>

      <p className="mt-2 text-3xl font-bold text-[#0885c0]">
        cargando... {progress}%
      </p>
    
      <p className="mt-8 text-2xl font-semibold text-[#0885c0]">
        {datoCurioso}
      </p>
    </div>
  );
};

export default ToothLoader;