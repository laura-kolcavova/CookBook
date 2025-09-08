import React from 'react';

import { FaHouse, FaMagnifyingGlass, FaPlus, FaRegCircleUser } from 'react-icons/fa6';
import { NavIconLink } from './NavIconLink';
import { Pages } from '../../../navigation/pages';
import { useRouter } from '~/navigation/hooks/useRouter';
import { HeaderLogo, NavBar, NavItem, NavList, StyledHeader } from './styled';
import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';
import { Link } from 'react-router-dom';
import { UserIconButton } from './UserIconButton';

export const Header: React.FC = () => {
  const { isAuthenticated } = useAtomValue(userAtom);

  const { getActivePage } = useRouter();

  const activePage = getActivePage();

  return (
    <StyledHeader>
      <div className="container mx-auto flex items-center justify-between min-h-32">
        <HeaderLogo>
          <Link to={Pages.Home.paths[0]}>
            <img src="/logo.png" alt="CookBook" className="h-32" />
          </Link>
        </HeaderLogo>

        <NavBar>
          <NavList>
            <NavItem>
              <NavIconLink
                text="Home"
                icon={<FaHouse />}
                to={Pages.Home.paths[0]}
                isActive={activePage === Pages.Home}
              />
            </NavItem>

            <NavItem>
              <NavIconLink
                text="Explore"
                icon={<FaMagnifyingGlass />}
                to={Pages.Explore.paths[0]}
                isActive={activePage === Pages.Explore}
              />
            </NavItem>

            {isAuthenticated && (
              <NavItem>
                <NavIconLink
                  text="Add a recipe"
                  icon={<FaPlus />}
                  to={Pages.AddRecipe.paths[0]}
                  isActive={activePage === Pages.AddRecipe}
                />
              </NavItem>
            )}

            {!isAuthenticated && (
              <NavItem>
                <NavIconLink
                  text="Log In"
                  icon={<FaRegCircleUser />}
                  to={Pages.LogIn.paths[0]}
                  isActive={activePage === Pages.LogIn}
                />
              </NavItem>
            )}

            {isAuthenticated && (
              <NavItem>
                <UserIconButton />
              </NavItem>
            )}
          </NavList>
        </NavBar>
      </div>
    </StyledHeader>
  );
};
