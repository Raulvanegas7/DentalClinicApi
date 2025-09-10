// "use client"

import React from "react"
import { NavbarLangin } from "@/features/landing/components/NavbarLanding"
import { HeroLanding } from "@/features/landing/components/HeroLanding"
import { ServiciosLanding } from "@/features/landing/components/ServiciosLanding"



const Home: React.FC = ()=>{
   
    return (
        <>
        <div className="relative">
            <div className="absolute top-0 left-0 w-full z-10">
                <NavbarLangin />
            </div>
            <HeroLanding />
            <ServiciosLanding/>
        </div>
          
        </>
    )
}

export default Home