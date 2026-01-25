import React from 'react';
import { Navigate } from 'react-router-dom';
import type { PageDefinition } from '~/navigation/PageDefinition';
import { pages } from '~/navigation/pages';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';

export type ProtectedRouteProps = {
  page: PageDefinition;
};

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ page }) => {
  const { isAuthenticated } = useLoggedUser();

  if (!isAuthenticated) {
    return <Navigate to={pages.LogIn.paths[0]} />;
  }

  return <page.component />;
};
