import React from 'react';

const Navbar: React.FC = () => {
  return (
    <nav className="bg-blue-600 text-white px-4 py-3 flex justify-between items-center">
      <div className="text-xl font-bold">DentalClinic</div>
      <ul className="flex gap-4">
        <li>
          <a href="#" className="hover:text-gray-300">Inicio</a>
        </li>
        <li>
          <a href="#" className="hover:text-gray-300">Servicios</a>
        </li>
        <li>
          <a href="#" className="hover:text-gray-300">Contacto</a>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
