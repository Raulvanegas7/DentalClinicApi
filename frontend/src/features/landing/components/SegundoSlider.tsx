import React from "react"
import { BotonesLanding } from "./BotonesLanding"

export const SegundoSlider: React.FC = () => {

    return (
        <div
            className="relative h-[700px] w-full" // Agrega 'relative' para posicionar el overlay
            style={{
                 backgroundImage: "url('/88031139-3831-4f37-8e21-1fd9e5a2abd1 1.png')",
                backgroundSize: "cover",
                backgroundPosition: "top",
backgroundRepeat: "no-repeat"
            }}
        >
            {/* Este es el overlay negro semitransparente */}
            <div className="absolute inset-0 bg-black opacity-35"></div> 
            
            {/* Aquí iría cualquier texto o contenido que quieras sobre la imagen */}
            <div className="relative z-10 flex flex-col justify-start items-start ml-11 mt-11 h-full text-white p-4">
                            <h2 className="text-6xl font-bold text-white w-[700px] ml-11 mt-11 [text-shadow:2px_2px_6px_rgba(0,0,0,0.4)]">los mejores profesionales a tu disposición</h2>
                            <p className="mt-4 text-2xl w-[500px] ml-11 text-white [text-shadow:2px_2px_6px_rgba(0,0,0,0.4)]">personal altamente calificado para ofrecer servicios de todo tipo utilizando maquinaria de ultima generacion</p>
                            {/* Botones, etc. */}
                        
                        
                        </div>
        </div>
    )
}