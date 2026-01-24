import type { ReactNode } from 'react';
import React from 'react';
import { Link } from 'react-router-dom';

type NavIconLinkProps = {
  text: ReactNode | string;
  icon: ReactNode;
  to: string;
  isActive?: boolean;
  onClick?: () => void;
};

export const NavIconLink: React.FC<NavIconLinkProps> = ({ text, icon, to, onClick, isActive }) => {
  const color = isActive ? 'text-navlink-color-active' : 'text-navlink-color';

  return (
    <Link
      to={to}
      onClick={onClick}
      className={`py-1 px-6 flex flex-col justify-center items-center transition-colors duration-150
        ${color}
        hover:text-navlink-color-hover`}>
      <span className="mb-1">{icon}</span>
      <span className="text-center">{text}</span>
    </Link>
  );
};
