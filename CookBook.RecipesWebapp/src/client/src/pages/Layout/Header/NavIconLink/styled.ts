import styled from 'styled-components';

import { NavLink } from 'reactstrap';

export const StyledNavLink = styled(NavLink)`
  color: var(--navlink-color);

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  cursor: pointer;

  &:hover {
    color: var(--navlink-color-hover);
  }
`;

export const IconWrapper = styled.span`
  margin-bottom: 0.1rem;
  display: flex;
`;

export const TextWrapper = styled.span``;
