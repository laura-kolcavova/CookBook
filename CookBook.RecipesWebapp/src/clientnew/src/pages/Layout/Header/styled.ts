import { Navbar, NavbarBrand } from 'reactstrap';
import styled from 'styled-components';

export const StyledNavbar = styled(Navbar)`
  background-color: var(--navbar-background-color);
`;

export const StyledNavBrand = styled(NavbarBrand)`
  color: var(--navbrand-color) !important;
  font-weight: 600;
`;
