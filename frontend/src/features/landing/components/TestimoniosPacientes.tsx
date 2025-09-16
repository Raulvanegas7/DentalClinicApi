import React from "react";
import Image from "next/image";

const TestimoniosPacientes: React.FC = () => {
  const testimonios = [
    {
      nombre: "Mariana Torres",
      imagen: "/testimonio1.png",
      texto:
        "Mi experiencia con la Dra. Laura fue excelente. Me atendió con muchísima paciencia y dedicación. Desde el primer momento me sentí cómoda, y eso no es fácil en el dentista 😅. Se nota que sabe cómo tratar a los pacientes con cariño y profesionalismo. ¡Mi hija también quedó encantada con ella! Sin dudas la mejor elección para el cuidado dental infantil.",
    },
    {
      nombre: "Ricardo Fernández",
      imagen: "/testimonio2.png",
      texto:
        "Tenía mis dudas sobre empezar un tratamiento de ortodoncia a esta altura con 49 años de edad, pero el Dr. Luis me transmitió total confianza. Es súper claro para explicar cada paso y se nota que tiene años de experiencia. Estoy muy conforme con los resultados que estoy viendo, y además siempre me atiende con buena onda y puntualidad. ¡Recomendado al 100%! ",
    },
    {
      nombre: "Carla Méndez",
      imagen: "/testimonio3.png",
      texto:
        "Llegué con un dolor terrible y bastante nerviosa, pero la Dra. Camila me tranquilizó desde el primer minuto. Fue súper clara explicando el tratamiento de conducto y todo el procedimiento fue más rápido y menos doloroso de lo que imaginaba. Tiene una mano increíble y se nota que ama lo que hace. ¡Gracias por salvar mi muela y mi semana! 😅",
    },{
      nombre: "Carla Méndez",
      imagen: "/testimonio3.png",
      texto:
        "Llegué con un dolor terrible y bastante nerviosa, pero la Dra. Camila me tranquilizó desde el primer minuto. Fue súper clara explicando el tratamiento de conducto y todo el procedimiento fue más rápido y menos doloroso de lo que imaginaba. Tiene una mano increíble y se nota que ama lo que hace. ¡Gracias por salvar mi muela y mi semana! 😅",
    },
  ];

  return (
    <div className="max-w-[1200px] mx-auto px-4 py-12">
      {/* Header */}
      <div
        className="flex flex-col justify-center items-center mb-12 w-full h-[230px] bg-center bg-no-repeat bg-contain"
        style={{ backgroundImage: "url('/imagenTestimonios.png')" }}
      >
        <h2
          id="testimonios"
          className="text-3xl md:text-5xl text-gray-600 text-center mt-24"
        >
          testimonios de pacientes{" "}
          <span className="font-bold text-[#169EDD]">
            felices y satisfechos
          </span>
        </h2>
        <h3 className="font-medium text-gray-500 text-lg md:text-xl text-center">
          algunos testimonios de personas que fueron atendidas en Dental Clinic.
        </h3>
      </div>

      {/* Grid de tarjetas */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-11">
        {testimonios.map((t, i) => (
          <div
            key={i}
            className="relative flex flex-col justify-between rounded-2xl shadow-xl overflow-hidden transform transition-transform duration-500 hover:scale-105"
            style={{
              background: "linear-gradient(to bottom, #377ACD, #169EDD)",
            }}
          >
            <div className="relative p-6 flex flex-col justify-between text-white z-10">
              {/* Header con imagen y nombre */}
              <div className="flex items-center gap-6 mb-4">
                <div className="overflow-hidden rounded-full w-[70px] h-[70px] border-4 border-white transition-transform duration-500 hover:scale-110">
                  <Image
                    src={t.imagen}
                    alt={t.nombre}
                    width={70}
                    height={70}
                    className="object-cover w-[70px] h-[70px] rounded-full"
                  />
                </div>
                <h3 className="text-2xl md:text-3xl font-bold">{t.nombre}</h3>
              </div>

              {/* Testimonio */}
              <p className="text-lg mb-16 pb-5">“{t.texto}”</p>

              {/* Estrellas alineadas con efecto moderno */}
              <div className="absolute bottom-6 left-1/2 transform -translate-x-1/2 flex justify-center gap-2 bg-white  backdrop-blur-md px-4 py-2 rounded-full shadow-lg">
                {[...Array(5)].map((_, idx) => (
                  <span
                    key={idx}
                    className="text-yellow-400 text-3xl md:text-4xl transition-transform duration-300 hover:scale-125 hover:text-yellow-300"
                  >
                    ★
                  </span>
                ))}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default TestimoniosPacientes;
