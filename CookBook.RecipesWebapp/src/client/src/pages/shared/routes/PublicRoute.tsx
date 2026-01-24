import React from 'react';
import { Navigate } from 'react-router-dom';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';
import type { PageDefinition } from '~/navigation/PageDefinition';

import { pages } from '~/navigation/pages';

export type PublicRouteProps = {
  page: PageDefinition;
};

export const PublicRoute: React.FC<PublicRouteProps> = ({ page }) => {
  const { isAuthenticated } = useLoggedUser();

  if (isAuthenticated && page === pages.LogIn) {
    return <Navigate to={pages.Home.paths[0]} />;
  }

  return <page.component />;
};
