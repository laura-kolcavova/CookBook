import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';

import { UserIdentityContext } from 'src/contexts/UserIdentityContext';
import { IPage } from 'src/navigation/models';
import { Pages } from 'src/navigation/pages';

export type ProtectedRouteProps = {
  page: IPage;
};

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ page }) => {
  const { user } = useContext(UserIdentityContext);

  if (!user.isAuthenticated) {
    return <Navigate to={Pages.LogIn.paths[0]} />;
  }

  return <page.component />;
};
