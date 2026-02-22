import React from 'react';
import { Navigate } from 'react-router-dom';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import type { PageDefinition } from '~/navigation/PageDefinition';
import { pages } from '~/navigation/pages';

export type ProtectedRouteProps = {
  page: PageDefinition;
};

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ page }) => {
  const { currentUser } = useCurrentUser();

  if (!currentUser.isAuthenticated) {
    return <Navigate to={pages.LogIn.paths[0]} />;
  }

  return <page.component />;
};
