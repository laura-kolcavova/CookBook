import styled, { css } from 'styled-components';
import { NavLink } from 'reactstrap';

type StyledNavLinkProps = {
  isActive: boolean;
};

export const StyledNavLink = styled(NavLink)<StyledNavLinkProps>`
  color: var(--navlink-color);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  cursor: pointer;

  ${(props) =>
    props.isActive &&
    css`
      color: var(--navlink-color-active);
    `}

  &:hover {
    color: var(--navlink-color-hover);
  }

  &:focus {
    color: var(--navlink-color);
  }
`;

export const IconWrapper = styled.span`
  margin-bottom: 0.1rem;
  display: flex;
`;

export const TextWrapper = styled.span``;
