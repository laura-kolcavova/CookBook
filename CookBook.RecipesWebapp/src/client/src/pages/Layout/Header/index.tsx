import React, { useContext } from 'react';
import { Nav, NavItem, Navbar, NavbarBrand } from 'reactstrap';
import { UserIdentityContext } from '../../../contexts/UserIdentityContext';

import { FaBookmark, FaHouse, FaMagnifyingGlass, FaRegCircleUser } from 'react-icons/fa6';
import { NavIconLink } from './NavIconLink';
import { Pages } from '../../../navigation/pages';
import { UserMenuItem } from './UserMenuItem';
import { useRouter } from '~/navigation/hooks/useRouter';

export const Header: React.FC = () => {
  const { user } = useContext(UserIdentityContext);
  const { goToPage } = useRouter();

  return (
    <Navbar container={true} sticky="top">
      <NavbarBrand
        onClick={() => {
          goToPage(Pages.Home);
        }}>
        Cook Book Recipes
      </NavbarBrand>
      <Nav className="ms-auto">
        <NavItem>
          <NavIconLink
            onClick={() => {
              goToPage(Pages.Home);
            }}
            text="Home"
            icon={<FaHouse />}
          />
        </NavItem>
        <NavItem>
          <NavIconLink
            onClick={() => {
              goToPage(Pages.Explore);
            }}
            text="Explore"
            icon={<FaMagnifyingGlass />}
          />
        </NavItem>
        {user.isAuthenticated && (
          <NavItem>
            <NavIconLink
              onClick={() => {
                goToPage(Pages.Saved);
              }}
              text="Saved"
              icon={<FaBookmark />}
            />
          </NavItem>
        )}
        {!user.isAuthenticated && (
          <NavItem>
            <NavIconLink
              onClick={() => {
                goToPage(Pages.LogIn);
              }}
              text="Log In"
              icon={<FaRegCircleUser />}
            />
          </NavItem>
        )}
        {user.isAuthenticated && <UserMenuItem />}
      </Nav>
    </Navbar>
  );
};
