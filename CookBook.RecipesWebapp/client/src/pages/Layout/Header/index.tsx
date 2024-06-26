import React, { useContext } from 'react';
import { Nav, NavItem, Navbar, NavbarBrand } from 'reactstrap';
import { UserIdentityContext } from 'src/contexts/UserIdentityContext';
import { Pages } from 'src/navigation/pages';
import { FaBookmark, FaHouse, FaMagnifyingGlass, FaRegCircleUser } from 'react-icons/fa6';
import { NavIconLink } from './NavIconLink';

export const Header: React.FC = () => {
  const { user } = useContext(UserIdentityContext);

  return (
    <Navbar container={true} sticky="top">
      <NavbarBrand href={Pages.Home.paths[0]}>Cook Book Recipes</NavbarBrand>
      <Nav className="ms-auto">
        <NavItem>
          <NavIconLink href={Pages.Home.paths[0]} text="Home" icon={<FaHouse />} />
        </NavItem>
        <NavItem>
          <NavIconLink href={Pages.Explore.paths[0]} text="Explore" icon={<FaMagnifyingGlass />} />
        </NavItem>
        {user.isAuthenticated && (
          <NavItem>
            <NavIconLink href={Pages.Saved.paths[0]} text="Saved" icon={<FaBookmark />} />
          </NavItem>
        )}
        {!user.isAuthenticated && (
          <NavItem>
            <NavIconLink href={Pages.LogIn.paths[0]} text="Log In" icon={<FaRegCircleUser />} />
          </NavItem>
        )}
      </Nav>
    </Navbar>
  );
};
