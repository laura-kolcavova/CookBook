import React from 'react';
import { Nav, NavItem } from 'reactstrap';

import { FaBookmark, FaHouse, FaMagnifyingGlass, FaRegCircleUser } from 'react-icons/fa6';
import { NavIconLink } from './NavIconLink';
import { Pages } from '../../../navigation/pages';
import { UserMenuItem } from './UserMenuItem';
import { useRouter } from '~/navigation/hooks/useRouter';
import { StyledNavbar, StyledNavBrand } from './styled';
import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';

export const Header: React.FC = () => {
  const { isAuthenticated } = useAtomValue(userAtom);

  const { goToPage } = useRouter();

  return (
    <header>
      <StyledNavbar sticky="top" container="lg">
        <StyledNavBrand
          onClick={() => {
            goToPage(Pages.Home);
          }}>
          Cook Book Recipes
        </StyledNavBrand>
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
          {isAuthenticated && (
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
          {!isAuthenticated && (
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
          {isAuthenticated && <UserMenuItem />}
        </Nav>
      </StyledNavbar>
    </header>
  );
};
