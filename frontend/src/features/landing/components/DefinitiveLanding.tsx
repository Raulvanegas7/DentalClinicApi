// "use client"

import React from "react"
import { NavbarLangin } from "@/features/landing/components/NavbarLanding"
import { HeroLanding } from "@/features/landing/components/HeroLanding"
import { ServiciosLanding } from "@/features/landing/components/ServiciosLanding"
import Profesionales from "@/features/landing/components/Profesionales"
import TestimoniosPacientes from "@/features/landing/components/TestimoniosPacientes"
import Contacto from "@/features/landing/components/Contacto"
import CallToAction from "@/features/landing/components/CallToAction"
const Home: React.FC = ()=>{
   
    return (
        <>
        <div className="relative">
            <div className="absolute top-0 left-0 w-full ">
                <NavbarLangin />
            </div>
            <HeroLanding />
            <ServiciosLanding/>
            <Profesionales/>
            <TestimoniosPacientes/>
            <Contacto/>
            <CallToAction/>
        </div>
          
        </>
    )
}

export default Home