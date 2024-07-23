import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';

import { UserIdentityContext } from 'src/contexts/UserIdentityContext';
import { IPage } from 'src/navigation/models';
import { Pages } from 'src/navigation/pages';

export type PublicRouteProps = {
  page: IPage;
};

export const PublicRoute: React.FC<PublicRouteProps> = ({ page }) => {
  const { user } = useContext(UserIdentityContext);

  if (user.isAuthenticated && page === Pages.LogIn) {
    return <Navigate to={Pages.Home.paths[0]} />;
  }

  return <page.component />;
};
