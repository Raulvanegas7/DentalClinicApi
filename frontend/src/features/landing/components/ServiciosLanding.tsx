import React from "react";
import Image from "next/image";
import { Tooth } from "healthicons-react";

export const ServiciosLanding: React.FC = () => {
  return (
    <>
      {/* Encabezado */}
      <div
        className="h-[180px] w-[700px] m-auto mt-25 flex flex-col justify-center items-center"
        style={{
          backgroundImage: "url('/Diente2.png')",
          backgroundSize: "contain",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
        }}
      >
        <h2
          id="servicios"
          className="font-medium text-gray-600 text-5xl text-center"
        >
          nuestros{" "}
          <span className="font-bold text-5xl text-[#169EDD]">servicios</span>
        </h2>
        <h3 className="font-medium text-gray-500 text-xl text-center">
          Blanqueamiento, ortodoncia, limpieza y más para una sonrisa saludable
        </h3>
      </div>

      {/* Contenedor de tarjetas */}
      <div className="flex flex-col gap-10 mt-11 mb-11 bg-white p-5">
        {/* primera fila */}
        <div className="flex justify-center items-center gap-9 m-2 flex-wrap">
          <CardServicio
            img="/Blanqueamiento.png"
            title="Blanqueamiento dental"
            desc="Tratamiento estético que aclara varios tonos el color de los dientes, eliminando manchas y devolviendo brillo a la sonrisa. Seguro, rápido y eficaz, se realiza en consultorio con tecnología profesional."
          />
          <CardServicio
            img="/Carillas.png"
            title="Colocación de carillas dentales"
            desc="Las carillas son finas láminas que se adhieren a la parte frontal de los dientes para mejorar su forma, color y alineación. Son una solución estética ideal para lograr una sonrisa armónica y perfecta."
          />
          <CardServicio
            img="/Caries.png"
            title="Tratamientos de caries"
            desc="Consiste en la eliminación del tejido dañado por la caries y su posterior restauración con materiales estéticos y duraderos, preservando la estructura dental y evitando complicaciones mayores."
          />
        </div>

        {/* segunda fila */}
        <div className="flex justify-center items-center gap-9 m-2 flex-wrap">
          <CardServicio
            img="/DolorMuelas.png"
            title="Atención de dolores de muelas"
            desc="Diagnóstico y tratamiento del dolor dental, que puede deberse a caries profundas, infecciones o problemas en las encías. Brindamos soluciones rápidas para aliviar el malestar dental."
          />
          <CardServicio
            img="/Brackets.png"
            title="Ortodoncia con brackets"
            desc="Tratamiento para corregir la posición de los dientes y la mordida mediante el uso de brackets metálicos, cerámicos o alineadores. Mejora tanto la estética como la funcionalidad de la boca."
          />
          <CardServicio
            img="/Limpieza.png"
            title="Limpieza bucal profesional"
            desc="Eliminación de placa bacteriana, sarro y manchas superficiales. Ayuda a prevenir enfermedades como la gingivitis o periodontitis, manteniendo encías y dientes saludables."
          />
        </div>  
      </div>
    </>
  );
};

// Componente de tarjeta individual
const CardServicio: React.FC<{ img: string; title: string; desc: string }> = ({
  img,
  title,
  desc,
}) => {
  return (
    <div className="flex flex-col justify-start items-center gap-0 w-[350px] m-2 pb-8 rounded-xl bg-white shadow-lg hover:shadow-2xl transition-transform duration-500 ease-in-out hover:scale-105">
      <div className="overflow-hidden rounded-t-xl">
        <Image
          src={img}
          alt={`servicio: ${title}`}
          width={380}
          height={220}
          className="w-[380px] h-[220px] object-cover transition-transform duration-500 ease-in-out hover:scale-110"
        />
      </div>
      <Tooth
        size={50}
        color="#FFFF"
        className="rounded-4xl w-[50px] h-[50px] p-2 bg-[#169EDD] mt-3 shadow-md"
      />
      <h2 className="font-bold text-xl text-center mt-5 text-[#169EDD]">
        {title}
      </h2>
      <p className="text-lg text-center text-gray-500 px-4">{desc}</p>
    </div>
  );
};
