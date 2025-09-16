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
        "Tenía mis dudas sobre empezar un tratamiento de ortodoncia a esta altura con 49 años de edad, pero el Dr. Luis me transmitió total confianza. Es súper claro para explicar cada paso y se nota que tiene años de experiencia. Estoy muy conforme con los resultados que estoy viendo, y además siempre me atiende con buena onda y puntualidad. ¡Recomendado al 100%!",
    },
    {
      nombre: "Carla Méndez",
      imagen: "/testimonio3.png",
      texto:
        "Llegué con un dolor terrible y bastante nerviosa, pero la Dra. Camila me tranquilizó desde el primer minuto. Fue súper clara explicando el tratamiento de conducto y todo el procedimiento fue más rápido y menos doloroso de lo que imaginaba. Tiene una mano increíble y se nota que ama lo que hace. ¡Gracias por salvar mi muela y mi semana! 😅",
    },
    {
      nombre: "Mariana Torres",
      imagen: "/testimonio1.png",
      texto:
        "Mi experiencia con la Dra. Laura fue excelente. Me atendió con muchísima paciencia y dedicación. Desde el primer momento me sentí cómoda, y eso no es fácil en el dentista 😅. Se nota que sabe cómo tratar a los pacientes con cariño y profesionalismo. ¡Mi hija también quedó encantada con ella! Sin dudas la mejor elección para el cuidado dental infantil.",
    },
  ];

  return (
    <div className="max-w-[1200px] mx-auto px-4 py-12">
        
        <div className="flex flex-col justify-center items-center mb-12 w-[1200px] h-[230px] m-auto"
        style={{
          backgroundImage: "url('/imagenTestimonios.png')",
          backgroundSize: "contain",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
        }}
        >
          <h2 id="testimonios" className="text-4xl font-semibold text-gray-600 text-center mt-24">
        testimonios de pacientes{" "}
        <span className="font-bold text-[#169EDD]">felices y satisfechos</span>
      </h2>
      <h3 className="font-medium text-gray-500 text-xl text-center ">
       algunos testimonios de personas que fueron atendidas en Dental Clinic.
      </h3>
        </div>

      {/* Grid de tarjetas */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-11">
        {testimonios.map((t, i) => (
          <div
            key={i}
            className="bg-[#169EDD] text-white rounded-2xl shadow-lg p-6 flex flex-col justify-between relative"
          >
            {/* Header con imagen y nombre */}
            <div className="flex justify-start items-center w-[450px] gap-24 mb-2 ">
              <Image
                src={t.imagen}
                alt={t.nombre}
                width={60}
                height={60}
                className="rounded-full border-4 mb-3 border-white w-[70px] h-[70px] object-cover"
              />
              <h3 className="text-3xl font-bold">{t.nombre}</h3>
            </div>

            {/* Testimonio */}
            <p className=" text-white mb-8 text-lg">“{t.texto}”</p>

            {/* Caja blanca con estrellas */}
            <div className="bg-white rounded-lg px-4 py-2 flex justify-center gap-1 absolute -bottom-5 left-1/2 transform -translate-x-1/2 shadow-md">
              {[...Array(5)].map((_, i) => (
                <span key={i} className="text-yellow-400 font-semibold text-4xl">★</span>
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default TestimoniosPacientes;
