import { FC } from "react";
import Image from "next/image";
import { MapPin, Phone, Mail, FileText, Users, Briefcase, Instagram, Facebook, Youtube, Music, Home, MessageSquare } from "lucide-react";

const Footer: FC = () => {
  // Enlaces de Guía rápida (coinciden con tu navbar)
  const quickLinks = [
    { href: "/#inicio", label: "Inicio", Icon: Home },
    { href: "/#servicios", label: "Servicios", Icon: Briefcase },
    { href: "/#equipo", label: "Nuestro equipo", Icon: Users },
    { href: "/#testimonios", label: "Testimonios", Icon: MessageSquare },
    { href: "/#contacto", label: "Contáctanos", Icon: Phone },
  ];

  return (
    <footer className="bg-gradient-to-b from-sky-700 to-sky-500 text-white rounded-t-3xl mt-28 shadow-inner">
      <div className="max-w-6xl mx-auto px-6 py-12 grid gap-8 md:grid-cols-4 border-b border-white/30">

        {/* Contacto */}
        <div className="pt-6 md:pt-0 px-0 md:px-4 transition-colors hover:bg-white/10 rounded-lg flex flex-col gap-3">
          <h3 className="flex items-center gap-2 font-extrabold text-2xl mb-1">
            <MapPin size={24} /> Contacto
          </h3>
          <p className="font-semibold text-lg">Dirección:</p>
          <p className="mb-2">Av. Libertad 1450, Piso 2 – Consultorio 203 – CABA</p>
          <p className="font-semibold text-lg mt-2">Teléfono:</p>
          <p className="mb-2 flex items-center gap-2"><Phone size={18} /> +54-9-113-456-7890</p>
          <p className="font-semibold text-lg mt-2">Email:</p>
          <p className="mb-2 flex items-center gap-2"><Mail size={18} /> DentalClinic@gmail.com</p>
          <p className="font-semibold text-lg mt-2">Horarios de atención:</p>
          <p>Lunes a Viernes 9:00–18:00<br />Sábados 9:00–13:00</p>
        </div>

        {/* Guía rápida */}
        <div className="pt-6 md:pt-0 px-0 md:px-4 transition-colors  hover:bg-white/10 rounded-lg border-t md:border-t-0 md:border-l border-white/30">
          <h3 className="flex items-center gap-2 font-extrabold text-2xl mb-4">
            <FileText size={24} /> Guía rápida
          </h3>
          <ul className="space-y-3 text-lg">
            {quickLinks.map(({ href, label, Icon }) => (
              <li key={href}>
                <a
                  href={href}
                  className="flex items-center gap-2 transition-colors duration-300 hover:text-blue-300"
                >
                  <Icon size={16} /> {label}
                </a>
              </li>
            ))}
          </ul>
        </div>

        {/* Legal */}
        <div className="pt-6 md:pt-0 px-0 md:px-4 transition-colors hover:bg-white/10 rounded-lg border-t md:border-t-0 md:border-l border-white/30">
          <h3 className="flex items-center gap-2 font-extrabold text-2xl mb-4">
            <Briefcase size={24} /> Legal
          </h3>
          <ul className="space-y-2 text-lg">
            <li>
              <a href="#" className="flex items-center gap-2 transition-colors duration-300 hover:text-blue-300">
                <FileText size={16} /> Política de privacidad
              </a>
            </li>
            <li>
              <a href="#" className="flex items-center gap-2 transition-colors duration-300 hover:text-blue-300">
                <FileText size={16} /> Términos y condiciones
              </a>
            </li>
          </ul>
          <p className="mt-4 text-sm text-white/90">
            La información en este sitio no sustituye la consulta profesional.
          </p>
        </div>

        {/* Postúlate & Redes */}
        <div className="pt-6 md:pt-0 px-0 md:px-4 transition-colors w-[310px] hover:bg-white/10 rounded-lg border-t md:border-t-0 md:border-l border-white/30">
          <h3 className="flex items-center gap-2 font-extrabold text-2xl mb-4">
            <Briefcase size={24} /> Postúlate aquí
          </h3>
          <p className="text-lg"><span className="font-semibold">Dentista:</span> estudios universitarios comprobables</p>
          <p className="text-lg mt-1"><span className="font-semibold">Recepcionista:</span> experiencia en atención al cliente comprobable</p>

          <h3 className="flex items-center gap-2 font-extrabold text-2xl mt-10 mb-3">
            <Users size={24} /> Nuestras redes
          </h3>
          <ul className="space-y-3 text-lg">
            <li className="flex items-center gap-2"><Instagram size={18} /> Instagram: Dental_Clinic</li>
            <li className="flex items-center gap-2"><Music size={18} /> TikTok: Dental_Clinic</li>
            <li className="flex items-center gap-2"><Facebook size={18} /> Facebook: Dental_Clinic_oficial</li>
            <li className="flex items-center gap-2"><Youtube size={18} /> YouTube: dentalClinic2237</li>
          </ul>
        </div>
      </div>

      {/* Línea divisoria inferior */}
      <hr className="border-white/40" />

      {/* Bottom bar */}
      <div className="max-w-6xl mx-auto px-6 py-6 flex flex-col md:flex-row items-center justify-between gap-4">
        <div className="flex justify-center md:justify-start items-center gap-4 w-full md:w-auto">
          <Image
            src="/LogoDentalClini.png"
            alt="Dental Clinic"
            width={200}
            height={200}
            className="bg-white object-cover  w-[200px] h-[85px] "
          />
        </div>

        <p className="text-base text-center md:text-left text-white/90 flex-1">
          © 2025–2026 DentalClinic. Todos los derechos reservados. | Diseño y desarrollo: Raúl Vanegaz &amp; Matías Díaz.
        </p>

        <div className="flex justify-center md:justify-end items-center w-full md:w-auto">
          <select className="bg-white text-sky-700 rounded px-3 py-1 text-base hover:bg-white/80 transition-colors">
            <option>Español</option>
            <option>English</option>
          </select>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
