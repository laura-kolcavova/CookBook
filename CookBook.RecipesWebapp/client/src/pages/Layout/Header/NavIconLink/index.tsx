import React from 'react';

import { IconWrapper, StyledNavLink, TextWrapper } from './styled';

interface INavIconLinkProps {
  href: string;
  text: string;
  icon: JSX.Element;
}

export const NavIconLink: React.FC<INavIconLinkProps> = ({ href, text, icon }) => {
  return (
    <StyledNavLink href={href}>
      <IconWrapper>{icon}</IconWrapper>
      <TextWrapper> {text}</TextWrapper>
    </StyledNavLink>
  );
};
