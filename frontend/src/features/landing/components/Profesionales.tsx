import React from "react";
import Image from "next/image";

const Profesionales: React.FC = () => {
  return (
    <>
      {/* Encabezado */}
      <div
        className="h-[180px] w-full m-auto mt-20 flex flex-col justify-center items-center mb-20"
        style={{
          backgroundImage: "url('/Dentista2.png')",
          backgroundSize: "contain",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
        }}
      >
        <h2 className="font-medium text-gray-600 text-5xl mt-20 text-center">
          conoce a nuestros{" "}
          <span className="font-bold text-5xl text-[#169EDD]">
            profesionales
          </span>
        </h2>
        <h3
          id="equipo"
          className="font-medium w-full text-gray-500 text-xl text-center px-4"
        >
          DentalClinic cuenta con profesionales altamente capacitados para
          brindarte la mejor atención dental.
        </h3>
      </div>

      {/* Contenedor de tarjetas */}
      <div className="max-w-[1300px] mx-auto grid grid-cols-1 md:grid-cols-2 gap-8 mb-20 px-4">
        {/* Camila */}
        <div className="relative group rounded-2xl shadow-xl overflow-hidden cursor-pointer">
          {/* Capa Inicial con Imagen de Fondo y Texto */}
          <div className="absolute inset-0 z-10 flex items-center justify-center transition-opacity duration-500 group-hover:opacity-0">
            <Image
              src="/CamilaRios.png"
              alt="Dra. Camila Ríos"
              fill
              className="object-cover object-[10%_15%]"
            />
            <div className="absolute inset-0 bg-black/30"></div>
            <div className="relative text-center text-white z-20">
              <h2 className="font-bold text-xl drop-shadow-lg">
                Dra. Camila Ríos
              </h2>
              <span className="text-[#169EDD] font-bold drop-shadow-lg">
                Especialista en Endodoncia
              </span>
            </div>
          </div>
          {/* Contenedor del hover (se desplaza) */}
          <div className="flex bg-white rounded-2xl shadow-xl transition-transform duration-500 translate-x-[220px] group-hover:translate-x-0 z-0">
            <Image
              src="/CamilaRios.png"
              alt="Dra. Camila Ríos"
              width={220}
              height={100}
              className="w-[220px] object-cover flex-shrink-0"
            />
            <div className="flex flex-col gap-3 p-6 border-r-6 border-[#169EDD]">
              <h2 className="text-gray-700 font-bold text-xl">
                Dra. Camila Ríos ~{" "}
                <span className="text-[#169EDD] font-bold">
                  Especialista en Endodoncia
                </span>
              </h2>
              <h3 className="text-gray-500 italic text-lg">
                “Mi prioridad es salvar tus piezas dentales y cuidar tu salud
                desde la raíz.”
              </h3>
              <p className="text-gray-600 text-md">
                Con más de 8 años de experiencia en endodoncia, la Dra. Camila
                Ríos es reconocida por su precisión y calidez. Su enfoque está
                en aliviar el dolor dental y preservar la funcionalidad de cada
                diente, realizando tratamientos de conducto con tecnología
                moderna y máxima comodidad para el paciente.
              </p>
            </div>
          </div>
        </div>

        {/* Luis */}
        <div className="relative group rounded-2xl shadow-xl overflow-hidden cursor-pointer">
          {/* Capa Inicial con Imagen de Fondo y Texto */}
          <div className="absolute inset-0 z-10 flex items-center justify-center transition-opacity duration-500 group-hover:opacity-0 ">
            <Image
              src="/LuisGomez.png"
              alt="Dr. Luis Gómez"
              fill
              className="object-cover object-[10%_15%]"
            />
            <div className="absolute inset-0 bg-black/30 bg-opacity-50"></div>
            <div className="relative text-center text-white z-20">
              <h2 className="font-bold text-xl drop-shadow-lg">
                Dr. Luis Gómez
              </h2>
              <span className="text-[#169EDD] font-bold drop-shadow-lg">
                Especialista en Ortodoncia
              </span>
            </div>
          </div>
          {/* Contenedor del hover (se desplaza) */}
          <div className="flex bg-white rounded-2xl shadow-xl transition-transform duration-500 translate-x-[220px] group-hover:translate-x-0 z-0">
            <Image
              src="/LuisGomez.png"
              alt="Dr. Luis Gómez"
              width={220}
              height={100}
              className="w-[220px] object-cover flex-shrink-0  "
            />
            <div className="flex flex-col gap-3 p-6 border-r-6 border-[#169EDD]">
              <h2 className="text-gray-700 font-bold text-xl">
                Dr. Luis Gómez ~{" "}
                <span className="text-[#169EDD] font-bold">
                  Especialista en Ortodoncia
                </span>
              </h2>
              <h3 className="text-gray-500 italic text-lg">
                “Una sonrisa alineada no solo es estética, es también salud.”
              </h3>
              <p className="text-gray-600 text-md">
                El Dr. Luis Gómez cuenta con más de 15 años de trayectoria en
                ortodoncia, acompañando a cientos de pacientes en su camino
                hacia una sonrisa armónica y funcional. Desde brackets
                convencionales hasta alineadores invisibles, su experiencia y
                compromiso aseguran resultados efectivos y personalizados.
              </p>
            </div>
          </div>
        </div>

        {/* Javier */}
        <div className="relative group rounded-2xl shadow-xl overflow-hidden cursor-pointer">
          <div className="absolute inset-0 z-10 flex items-center justify-center transition-opacity duration-500 group-hover:opacity-0">
            <Image
              src="/JavierVargas.png"
              alt="Dr. Javier Vargas"
              fill
              className="object-cover object-[10%_15%]"
            />
            <div className="absolute inset-0 bg-black/30 bg-opacity-50"></div>
            <div className="relative text-center text-white z-20">
              <h2 className="font-bold text-xl drop-shadow-lg">
                Dr. Javier Vargas
              </h2>
              <span className="text-[#169EDD] font-bold drop-shadow-lg">
                Dentista General
              </span>
            </div>
          </div>
          {/* Contenedor del hover (se desplaza) */}
          <div className="flex bg-white rounded-2xl shadow-xl transition-transform duration-500 translate-x-[220px] group-hover:translate-x-0 z-0">
            <Image
              src="/JavierVargas.png"
              alt="Dr. Javier Vargas"
              width={220}
              height={100}
              className="w-[220px] object-cover flex-shrink-0"
            />
            <div className="flex flex-col gap-3 p-6 border-r-6 border-[#169EDD]">
              <h2 className="text-gray-700 font-bold text-xl">
                Dr. Javier Vargas ~{" "}
                <span className="text-[#169EDD] font-bold">
                  Dentista General
                </span>
              </h2>
              <h3 className="text-gray-500 italic text-lg">
                “Una atención cercana y completa para toda la familia.”
              </h3>
              <p className="text-gray-600 text-md">
                El Dr. Javier Vargas es un profesional joven con sólida
                formación en odontología general. Se destaca por brindar una
                atención integral basada en la prevención, el diagnóstico
                temprano y el tratamiento eficaz de diversas patologías
                dentales. Su trato cálido y cercano es ideal para pacientes de
                todas las edades.
              </p>
            </div>
          </div>
        </div>

        {/* Laura */}
        <div className="relative group rounded-2xl shadow-xl overflow-hidden cursor-pointer">
          <div className="absolute inset-0 z-10 flex items-center justify-center transition-opacity duration-500 group-hover:opacity-0">
            <Image
              src="/LauraHerrera.png"
              alt="Dra. Laura Herrera"
              fill
              className="object-cover object-[10%_15%]"
            />
            <div className="absolute inset-0 bg-black/30 bg-opacity-50"></div>
            <div className="relative text-center text-white z-20">
              <h2 className="font-bold text-xl drop-shadow-lg">
                Dra. Laura Diaz
              </h2>
              <span className="text-[#169EDD] font-bold drop-shadow-lg">
                Odontopediatría
              </span>
            </div>
          </div>
          {/* Contenedor del hover (se desplaza) */}
          <div className="flex bg-white rounded-2xl shadow-xl transition-transform duration-500 translate-x-[220px] group-hover:translate-x-0 z-0">
            <Image
              src="/LauraHerrera.png"
              alt="Dra. Laura Herrera"
              width={220}
              height={100}
              className="w-[220px] object-cover flex-shrink-0"
            />
            <div className="flex flex-col gap-3 p-6 border-r-6 border-[#169EDD]">
              <h2 className="text-gray-700 font-bold text-xl">
                Dra. Laura Diaz ~{" "}
                <span className="text-[#169EDD] font-bold">
                  Odontopediatría
                </span>
              </h2>
              <h3 className="text-gray-500 italic text-lg">
                “Acompañar a los más chicos con confianza y empatía es la clave
                de una buena salud bucal.”
              </h3>
              <p className="text-gray-600 text-md">
                Especializada en odontopediatría, la Dra. Laura Herrera se
                dedica al cuidado dental de niños, creando un ambiente amable y
                sin miedo para que los más pequeños construyan hábitos
                saludables desde temprano. Su atención se adapta a cada etapa
                del crecimiento infantil.
              </p>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Profesionales;