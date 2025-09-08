import { Link } from 'react-router-dom';
import styled from 'styled-components';

type NavLinkProps = {
  isActive: boolean;
};
export const NavLink = styled(Link)<NavLinkProps>`
  color: ${(props) => (props.isActive ? 'var(--navlink-color-active)' : 'var(--navlink-color)')};
  padding: 0.2rem 1.5rem;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  &:hover {
    color: var(--navlink-color-hover);
  }
`;

export const IconWrapper = styled.span`
  margin-bottom: 0.2rem;
`;

export const TextWrapper = styled.span`
  text-align: center;
`;
