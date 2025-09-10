import React from "react"

export const TercerSlider: React.FC = () => {

    return (
        <div
            className="relative h-[700px] w-full" // Agrega 'relative' para posicionar el overlay
            style={{
                backgroundImage: "url('/NiñosSlider.png')",
                backgroundSize: "cover",
                backgroundPosition: "top",
                backgroundRepeat: "no-repeat"
            }}
        >
            {/* Este es el overlay negro semitransparente */}
            <div className="absolute inset-0 bg-black opacity-35"></div> 
            
            {/* Aquí iría cualquier texto o contenido que quieras sobre la imagen */}
            <div className="relative z-10 flex flex-col justify-start items-start ml-11 mt-11 h-full text-white p-4">
                <h2 className="text-6xl font-bold text-white w-[500px] ml-11 mt-11 [text-shadow:2px_2px_6px_rgba(0,0,0,0.4)]">atencion para niños de todas las edades</h2>
                <p className="mt-4 text-2xl w-[500px] ml-11 text-white [text-shadow:2px_2px_6px_rgba(0,0,0,0.4)]">Atencion especializada y personalizada para infantes desde los 5 a 13 años de edad</p>
            </div>
        </div>
    )
}
                    