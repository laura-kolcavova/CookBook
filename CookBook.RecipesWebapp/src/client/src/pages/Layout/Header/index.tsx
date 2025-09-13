import React from 'react';

import { FaHouse, FaMagnifyingGlass, FaPlus, FaRegCircleUser } from 'react-icons/fa6';
import { NavIconLink } from './NavIconLink';
import { Pages } from '../../../navigation/pages';
import { useRouter } from '~/navigation/hooks/useRouter';
import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';
import { Link } from 'react-router-dom';
import { UserIconButton } from './UserIconButton';

export const Header: React.FC = () => {
  const { isAuthenticated } = useAtomValue(userAtom);

  const { getActivePage } = useRouter();

  const activePage = getActivePage();

  return (
    <header className="header-background-color border-b border-solid header-border-color">
      <div className="container mx-auto flex items-center justify-between min-h-32">
        <div>
          <Link to={Pages.Home.paths[0]}>
            <img src="/logo.png" alt="CookBook" className="h-32" />
          </Link>
        </div>

        <nav>
          <ul className="flex list-none">
            <li>
              <NavIconLink
                text="Home"
                icon={<FaHouse />}
                to={Pages.Home.paths[0]}
                isActive={activePage === Pages.Home}
              />
            </li>

            <li>
              <NavIconLink
                text="Explore"
                icon={<FaMagnifyingGlass />}
                to={Pages.Explore.paths[0]}
                isActive={activePage === Pages.Explore}
              />
            </li>

            {isAuthenticated && (
              <li>
                <NavIconLink
                  text="Add a recipe"
                  icon={<FaPlus />}
                  to={Pages.AddRecipe.paths[0]}
                  isActive={activePage === Pages.AddRecipe}
                />
              </li>
            )}

            {!isAuthenticated && (
              <li>
                <NavIconLink
                  text="Log In"
                  icon={<FaRegCircleUser />}
                  to={Pages.LogIn.paths[0]}
                  isActive={activePage === Pages.LogIn}
                />
              </li>
            )}

            {isAuthenticated && (
              <li>
                <UserIconButton />
              </li>
            )}
          </ul>
        </nav>
      </div>
    </header>
  );
};
