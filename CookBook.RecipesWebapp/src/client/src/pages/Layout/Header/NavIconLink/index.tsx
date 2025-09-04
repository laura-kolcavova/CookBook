import React, { JSX } from 'react';

import { IconWrapper, StyledNavLink, TextWrapper } from './styled';

type NavIconLinkProps = {
  text: string;
  icon: JSX.Element;
  isActive: boolean;
  onClick: () => void;
};

export const NavIconLink: React.FC<NavIconLinkProps> = ({ onClick, text, icon, isActive }) => {
  return (
    <StyledNavLink onClick={onClick} tag="button" className={isActive && 'active'}>
      <IconWrapper>{icon}</IconWrapper>
      <TextWrapper> {text}</TextWrapper>
    </StyledNavLink>
  );
};
