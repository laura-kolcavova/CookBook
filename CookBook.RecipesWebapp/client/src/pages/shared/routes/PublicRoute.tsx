import React from 'react';
import { Navigate } from 'react-router-dom';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import type { PageDefinition } from '~/navigation/PageDefinition';

import { pages } from '~/navigation/pages';

export type PublicRouteProps = {
  page: PageDefinition;
};

export const PublicRoute: React.FC<PublicRouteProps> = ({ page }) => {
  const { currentUser } = useCurrentUser();

  if (currentUser.isAuthenticated && page === pages.LogIn) {
    return <Navigate to={pages.Home.paths[0]} />;
  }

  return <page.component />;
};
