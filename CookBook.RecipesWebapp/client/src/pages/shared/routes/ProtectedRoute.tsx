import React from 'react';
import { useLocation } from 'react-router-dom';
import { usersService } from '~/api/users/usersService';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import type { PageDefinition } from '~/navigation/PageDefinition';

export type ProtectedRouteProps = {
  page: PageDefinition;
};

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ page }) => {
  const { currentUser } = useCurrentUser();
  const location = useLocation();

  if (!currentUser.isAuthenticated) {
    const returnUrl = `${location.pathname}${location.search}${location.hash}`;

    const loginUrl = usersService.getLogInUserUrl(returnUrl);

    window.location.replace(loginUrl);

    return null;
  }

  return <page.component />;
};
