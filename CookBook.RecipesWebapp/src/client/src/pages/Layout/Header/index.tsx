import React from 'react';
import { Nav, NavItem } from 'reactstrap';

import { FaBookmark, FaHouse, FaMagnifyingGlass, FaPlus, FaRegCircleUser } from 'react-icons/fa6';
import { NavIconLink } from './NavIconLink';
import { Pages } from '../../../navigation/pages';
import { UserMenuItem } from './UserMenuItem';
import { useRouter } from '~/navigation/hooks/useRouter';
import { StyledNavbar, StyledNavBrand } from './styled';
import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';

export const Header: React.FC = () => {
  const { isAuthenticated } = useAtomValue(userAtom);

  const { goToPage, getActivePage } = useRouter();

  const activePage = getActivePage();

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
              text="Home"
              icon={<FaHouse />}
              onClick={() => {
                goToPage(Pages.Home);
              }}
              isActive={activePage === Pages.Home}
            />
          </NavItem>

          <NavItem>
            <NavIconLink
              text="Explore"
              icon={<FaMagnifyingGlass />}
              onClick={() => {
                goToPage(Pages.Explore);
              }}
              isActive={activePage === Pages.Explore}
            />
          </NavItem>

          {isAuthenticated && (
            <NavItem>
              <NavIconLink
                text="Add a recipe"
                icon={<FaPlus />}
                onClick={() => {
                  goToPage(Pages.AddRecipe);
                }}
                isActive={activePage === Pages.AddRecipe}
              />
            </NavItem>
          )}

          {isAuthenticated && (
            <NavItem>
              <NavIconLink
                text="Saved"
                icon={<FaBookmark />}
                onClick={() => {
                  goToPage(Pages.Saved);
                }}
                isActive={activePage === Pages.Saved}
              />
            </NavItem>
          )}

          {!isAuthenticated && (
            <NavItem>
              <NavIconLink
                text="Log In"
                icon={<FaRegCircleUser />}
                onClick={() => {
                  goToPage(Pages.LogIn);
                }}
                isActive={activePage === Pages.LogIn}
              />
            </NavItem>
          )}

          {isAuthenticated && <UserMenuItem />}
        </Nav>
      </StyledNavbar>
    </header>
  );
};
