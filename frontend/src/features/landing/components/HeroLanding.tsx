"use client"

import { Swiper, SwiperSlide } from "swiper/react";
import { Navigation, Pagination, Autoplay } from "swiper/modules";
import { BotonesLanding } from "./BotonesLanding";
// Importa los estilos de Swiper.js 
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';

import { PrimerSlider } from "./PrimerSlider";
import { SegundoSlider } from "./SegundoSlider";
import { TercerSlider } from "./TercerSlider";

export const HeroLanding: React.FC = () => {
    return (
        <section className="relative z-0 border pb-10  border-gray-300">
            <Swiper
                modules={[Navigation, Pagination, Autoplay]}
                spaceBetween={30}
                slidesPerView={1}
                navigation
                pagination={{ clickable: true }}
                autoplay={{ delay: 4000 }}
                loop
                className="w-full h-screen  " 
            >
                <SwiperSlide className="w-full h-full">
                    <PrimerSlider />
                </SwiperSlide>

                <SwiperSlide className="w-full h-full">
                    <SegundoSlider />
                </SwiperSlide>

                <SwiperSlide className="w-full h-full">
                    <TercerSlider />
                </SwiperSlide>

            </Swiper>
                <BotonesLanding />         
        </section>
    );
}