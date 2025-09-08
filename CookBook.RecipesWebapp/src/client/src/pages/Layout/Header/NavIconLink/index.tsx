import React, { type JSX } from 'react';

import { IconWrapper, NavLink, TextWrapper } from './styled';

type NavIconLinkProps = {
  text: string;
  icon: JSX.Element;
  to: string;
  isActive?: boolean;
  onClick?: () => void;
};

export const NavIconLink: React.FC<NavIconLinkProps> = ({ text, icon, to, onClick, isActive }) => {
  return (
    <NavLink to={to} onClick={onClick} isActive={isActive ?? false}>
      <IconWrapper>{icon}</IconWrapper>
      <TextWrapper>{text}</TextWrapper>
    </NavLink>
  );
};
