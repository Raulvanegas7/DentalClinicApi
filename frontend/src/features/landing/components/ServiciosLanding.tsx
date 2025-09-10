import React from "react";
import Image from "next/image";
import { Tooth } from "healthicons-react";


export const ServiciosLanding: React.FC = () => {

    return (
        <>
            <div
            className="h-[180px] w-[700px] m-auto mt-25 flex flex-col justify-center items-center " // Agrega 'relative' para posicionar el overlay
            style={{
                backgroundImage: "url('/Diente2.png')",
                backgroundSize: "contain",
                backgroundPosition: "center",
                backgroundRepeat: "no-repeat"
            }}>
                <h2 id="servicios" className="font-medium text-gray-600 text-5xl">nuestros <span className="font-bold text-5xl text-[#169EDD]">servicios</span></h2>
                <h3 className="font-medium text-gray-500 text-xl">Blanqueamiento, ortodoncia, limpieza y más para una sonrisa saludable</h3>
            </div>

            {/* Div contenedor de las tarjetitas */}
            <div className="flex flex-col gap-10 mt-11 mb-11 bg-white p-5">
                {/* primer fila */}
                <div className="flex justify-center items-center gap-9 m-2">

                    <div className="flex flex-col justify-start items-center gap-0  [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.3)] w-[350px] m-2 pb-8">
                        <Image src="/Blanqueamiento.png" alt="servicio: blanqueamiento dental" width={200} height={100} className="w-[380px] h-[220px]" />
                        <Tooth size={50} color="#FFFF" className="rounded-4xl w-[50px]  p-2 bg-[#169EDD] h-[50px] [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.6)] mt-2"></Tooth>
                        <h2 className="font-bold  text-xl text-center mt-5 text-[#169EDD]"> Blanqueamiento dental</h2>
                        <p className="text-lg text-center text-gray-500">Tratamiento estético que aclara varios tonos el color de los dientes, eliminando manchas y devolviendo brillo a la sonrisa. Seguro, rápido y eficaz, se realiza en consultorio con tecnología profesional.</p>
                    </div>

                    <div className="flex flex-col justify-start items-center gap-0  [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.3)]  w-[350px] m-2 pb-8">
                        <Image src="/Carillas.png" alt="servicio: Colocación de carillas" width={200} height={100} className="w-[380px] h-[220px]" />

                        <Tooth size={50} color="#FFFF" className="rounded-4xl w-[50px] p-2 bg-[#169EDD] h-[50px] [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.6)] mt-2"></Tooth>
                        
                        <h2 className="font-bold  text-xl text-center mt-5 text-[#169EDD]"> Colocación de carillas dentales</h2>
                        <p className="text-lg text-center text-gray-500">Las carillas son finas láminas que se adhieren a la parte frontal de los dientes para mejorar su forma, color y alineación. Son una solución estética ideal para lograr una sonrisa armónica y perfecta.</p>
                    </div>

                    <div className="flex flex-col justify-start items-center gap-0  [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.3)]  w-[350px] m-2 pb-8">
                        <Image src="/Caries.png" alt="servicio: Tratamientos de caries" width={200} height={100} className="w-[380px] h-[220px]" />
                         <Tooth size={50} color="#FFFF" className="rounded-4xl w-[50px] p-2 bg-[#169EDD] h-[50px] [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.6)] mt-2"></Tooth>
                        <h2 className="font-bold  text-xl text-center mt-5 text-[#169EDD]"> Tratamientos de caries</h2>
                        <p className="text-lg text-center text-gray-500">Consiste en la eliminación del tejido dañado por la caries y su posterior restauración con materiales estéticos y duraderos, preservando la estructura dental y evitando complicaciones mayores.</p>
                    </div>
                </div>

                {/* segunda fila */}
                <div className="flex justify-center items-center gap-9 m-2">

                    <div className="flex flex-col justify-start items-center gap-0 [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.3)]  w-[350px] m-2 pb-8">
                        <Image src="/DolorMuelas.png" alt="servicio: Atención de dolores de muelas" width={200} height={100} className="w-[380px] h-[220px]" />
                        <Tooth size={50} className="rounded-4xl w-[50px] p-2 bg-[#169EDD] h-[50px] [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.6)] mt-2"></Tooth>
                        <h2 className="font-bold  text-xl text-center mt-5 text-[#169EDD]">Atención de dolores de muelas</h2>
                        <p className="text-lg text-center text-gray-500">Diagnóstico y tratamiento del dolor dental, que puede deberse a caries profundas, infecciones o problemas en las encías. Brindamos soluciones rápidas para aliviar el malestar y preservar el diente.</p>
                    </div>

                    <div className="flex flex-col justify-start items-center gap-0 [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.3)] w-[350px] m-2 pb-8">
                        <Image src="/Brackets.png" alt="servicio:Ortodoncia con brackets" width={200} height={100} className="w-[380px] h-[220px]" />
                        <Tooth size={50} color="#FFFF" className="rounded-4xl w-[50px] p-2 bg-[#169EDD] h-[50px] [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.6)] mt-2"></Tooth>
                        <h2 className="font-bold  text-xl text-center mt-5 text-[#169EDD]">Ortodoncia con brackets</h2>
                        <p className="text-lg text-center text-gray-500">Tratamiento para corregir la posición de los dientes y la mordida mediante el uso de brackets metálicos, cerámicos o alineadores. Mejora tanto la estética como la funcionalidad de la boca.</p>
                    </div>

                    <div className="flex flex-col justify-start items-center gap-0 [box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.3)] w-[350px] m-2 pb-8">
                        <Image src="/Limpieza.png" alt="servicio:  Limpieza bucal profesional" width={200} height={100} className="w-[380px] h-[220px]" />
                        <Tooth size={50} color="#FFFF" className="[box-shadow:0px_-2px_4px_rgba(0,0,0,0.1),0px_2px_8px_rgba(0,0,0,0.6)] rounded-4xl w-[50px] p-2 bg-[#169EDD] h-[50px] mt-2"></Tooth>
                        <h2 className="font-bold  text-xl text-center mt-5 text-[#169EDD]">Limpieza bucal profesional</h2>
                        <p className="text-lg text-center text-gray-500">Eliminación de placa bacteriana, sarro y manchas superficiales. Ayuda a prevenir enfermedades como la gingivitis o periodontitis, manteniendo encías y dientes saludables.</p>
                    </div>
                </div>
            </div>
        </>
    )
        
}