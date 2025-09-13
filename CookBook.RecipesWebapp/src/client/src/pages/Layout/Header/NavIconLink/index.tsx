import React, { type JSX } from 'react';
import { Link } from 'react-router-dom';

type NavIconLinkProps = {
  text: string;
  icon: JSX.Element;
  to: string;
  isActive?: boolean;
  onClick?: () => void;
};

export const NavIconLink: React.FC<NavIconLinkProps> = ({ text, icon, to, onClick, isActive }) => {
  const color = isActive ? 'navlink-color-active' : 'navlink-color';

  return (
    <Link
      to={to}
      onClick={onClick}
      className={`py-1 px-6 flex flex-col justify-center items-center transition-colors duration-150
        ${color}
        navlink-color-hover`}>
      <span className="mb-1">{icon}</span>
      <span className="text-center">{text}</span>
    </Link>
  );
};
