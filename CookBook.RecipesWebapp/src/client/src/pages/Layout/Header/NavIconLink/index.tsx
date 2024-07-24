import React from 'react';

import { IconWrapper, StyledNavLink, TextWrapper } from './styled';

interface INavIconLinkProps {
  onClick: () => void;
  text: string;
  icon: JSX.Element;
}

export const NavIconLink: React.FC<INavIconLinkProps> = ({ onClick, text, icon }) => {
  return (
    <StyledNavLink onClick={onClick} tag="button">
      <IconWrapper>{icon}</IconWrapper>
      <TextWrapper> {text}</TextWrapper>
    </StyledNavLink>
  );
};
