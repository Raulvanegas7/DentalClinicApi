import React from "react";
import Link from "next/link";
import { useState } from "react";
import { ModalLanding } from "./ModalLanding";

export const BotonesLanding:React.FC = ()=>{

    const [isOpen, setIsOpen] = useState(false);

    return(
        <>
        <div className="flex absolute top-120 left-26 z-10 w-[390px] justify-between hover:scale-105 transition">
            <div className=" mt-5 ">
                <Link href="/home" className=" text-[#169EDD] text-2xl pt-5 pr-7 pb-5 pl-7 bg-white rounded-4xl font-bold shadow-2xl hover:bg-gray-200 hover:text-[#0a7db3] border hover:border-[#169EDD] transition">Saber más</Link>
            </div>
            <button onClick={() => setIsOpen(true)} className="mb-20 text-white text-2xl pt-5 pr-7 pb-5 pl-7 bg-[#169EDD]  rounded-4xl font-bold shadow-2xl hover:bg-[#0a7db3] hover:text-gray-200 transition">Sacar turno</button>

        </div>
            <ModalLanding isVisible={isOpen} onClose={() => {setIsOpen(false)}}/>
        </>
    )
}