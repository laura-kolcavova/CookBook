import React from 'react';
import { FormattedMessage, } from 'react-intl';

import { FaHouse, FaMagnifyingGlass, FaPlus, FaRegCircleUser } from 'react-icons/fa6';
import { NavIconLink } from './NavIconLink';
import { useRouter } from '~/navigation/hooks/useRouter';
import { Link } from 'react-router-dom';
import { UserIconButton } from './UserIconButton';
import { pages } from '~/navigation/pages';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';
import { messages } from './messages';

export const Header: React.FC = () => {
  const { isAuthenticated } = useLoggedUser();

  const { isPageActive } = useRouter();

  return (
    <header className="bg-header-background-color border-b border-solid border-header-border-color">
      <div className="container mx-auto px-4 flex items-center justify-between min-h-32">
        <div>
          <Link to={pages.Home.paths[0]}>
            <img src="/logo.png" alt="CookBook" className="h-32" />
          </Link>
        </div>

        <nav>
          <ul className="flex list-none">
            <li>
              <NavIconLink
                text={<FormattedMessage {...messages.home} />}
                icon={<FaHouse />}
                to={pages.Home.paths[0]}
                isActive={isPageActive(pages.Home)}
              />
            </li>

            <li>
              <NavIconLink
                text={<FormattedMessage {...messages.explore} />}
                icon={<FaMagnifyingGlass />}
                to={pages.Explore.paths[0]}
                isActive={isPageActive(pages.Explore)}
              />
            </li>

            {isAuthenticated && (
              <li>
                <NavIconLink
                  text={<FormattedMessage {...messages.addRecipe} />}
                  icon={<FaPlus />}
                  to={pages.AddRecipe.paths[0]}
                  isActive={isPageActive(pages.AddRecipe)}
                />
              </li>
            )}

            {!isAuthenticated && (
              <li>
                <NavIconLink
                  text={<FormattedMessage {...messages.logIn} />}
                  icon={<FaRegCircleUser />}
                  to={pages.LogIn.paths[0]}
                  isActive={isPageActive(pages.LogIn)}
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
