import React from "react"
import { BotonesLanding } from "./BotonesLanding"


export const PrimerSlider: React.FC = () => {

    return (
            <div
            className="relative h-[700px] w-full" // Agrega 'relative' para posicionar el overlay
            style={{
                backgroundImage: "url('/6f355d83-70cf-4fb9-9276-64fdda4d8ab2 1 (1).png')",
                backgroundSize: "cover",
                backgroundPosition: "center",
backgroundRepeat: "no-repeat"
            }}
        >
            {/* Este es el overlay negro semitransparente */}
            <div className="absolute inset-0 bg-black opacity-35"></div> 
            
            {/* Aquí iría cualquier texto o contenido que quieras sobre la imagen */}
            <div className="relative z-10 flex flex-col justify-start items-start ml-11 mt-11 h-full text-white p-4">
                <h2 className="text-6xl font-bold text-white w-[600px] ml-11 mt-11 [text-shadow:2px_2px_6px_rgba(0,0,0,0.4)]">tu sonrisa es lo más importante para nosotros</h2>
                <p className="mt-4 text-2xl w-[500px] ml-11 text-white [text-shadow:2px_2px_6px_rgba(0,0,0,0.4)]">atención personalizada, tecnología de vanguardia y un equipo que te cuida de verdad</p>
                {/* Botones, etc. */}
            
                
            
            </div>
        </div>
        
    )
}