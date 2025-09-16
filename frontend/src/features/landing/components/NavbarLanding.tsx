import React from "react"
import Link from "next/link"
import Image from "next/image"
import { Home, Briefcase, Users, MessageSquare, Phone } from "lucide-react" 

export const NavbarLangin: React.FC = () => {
    return (
        <nav className="bg-gray-100/95 fixed z-100 w-full flex flex-row justify-around items-center h-[91px] shadow-sm">
            {/* Logo */}
            <section>
                <Link href="/">
                    <Image
                        src="/LogoDentalClini.png"
                        alt="logo-DentalClinic"
                        className="w-[275px] h-[170px] mt-1 mr-11"
                        width={280}
                        height={200}
                    />
                </Link>
            </section>

            {/* Links */}
            <ul className="w-[60%] flex flex-row justify-between items-center pb-1 mr-11">
                {[
                    { href: "/#inicio", label: "inicio", Icon: Home },
                    { href: "/#servicios", label: "servicios", Icon: Briefcase },
                    { href: "/#equipo", label: "nuestro equipo", Icon: Users },
                    { href: "/#testimonios", label: "testimonios", Icon: MessageSquare },
                    { href: "/#contacto", label: "contactanos", Icon: Phone },
                ].map(({ href, label, Icon }) => (
                    <li key={href}>
                        <Link 
                            href={href} 
                            className="group relative flex items-center gap-2 text-[#169EDD] text-lg font-semibold transition-all duration-300 ease-in-out"
                        >
                            <Icon className="w-5 h-5 text-[#169EDD] transition-transform duration-300 group-hover:scale-110" />
                            <span className="transition-colors duration-300 group-hover:text-blue-600">
                                {label}
                            </span>
                            {/* underline animado */}
                            <span className="absolute left-0 -bottom-1 w-0 h-[2px] bg-blue-500 transition-all duration-300 group-hover:w-full rounded"></span>
                        </Link>
                    </li>
                ))}
            </ul>
        </nav>
    )
}
