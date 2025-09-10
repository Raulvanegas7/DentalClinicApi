import React from "react"
import Link from "next/link"
import Image from "next/image"
export const NavbarLangin: React.FC = () => {

    return (
        <nav className="bg-gray-100/95 fixed z-50 w-full flex flex-row justify-around items-center h-[90px]  ">
            <section>
                <Link href="/" >
                    <Image src="/LogoDentalClini.png" alt="logo-DentalClinic" className="w-[275px] h-[170px] mt-1 mr-11" width={280} height={200}/> 
                </Link>
            </section>
            <ul className="w-[60%] flex flex-row justify-between items-center pb-1 mr-11">
                <li>
                    <Link href="/" className="text-[#169EDD] text-xl font-semibold hover:border-b-5 border-b-blue-400 pb-8 pl-2 pr-2 pt-11">inicio</Link>
                </li>

                <li>
                    <Link href="/#servicios" className="text-[#169EDD] text-xl font-semibold hover:border-b-5 border-b-blue-400 pb-8 pt-11">servicios</Link>
                </li>

                <li>
                    <Link href="/#testimonios" className="text-[#169EDD] text-xl font-semibold hover:border-b-5 border-b-blue-400 pb-8 pt-11">testimonios</Link>
                </li>

                <li>
                    <Link href="/#equipo" className="text-[#169EDD] text-xl font-semibold hover:border-b-5 border-b-blue-400 pb-8 pt-11">nuestro equipo</Link>
                </li>

                <li>
                    <Link href="/#contacto"className="text-[#169EDD] text-xl font-semibold hover:border-b-5 border-b-blue-400 pb-8 pt-11">contactanos</Link>
                </li>
            </ul>
        </nav>
    )
}