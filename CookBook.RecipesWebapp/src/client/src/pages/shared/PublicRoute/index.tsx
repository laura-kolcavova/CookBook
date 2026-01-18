import { useAtomValue } from 'jotai';
import React from 'react';
import { Navigate } from 'react-router-dom';
import { userAtom } from '~/atoms/userAtom';
import type { PageDefinition } from '~/navigation/PageDefinition';

import { pages } from '~/navigation/pages';

export type PublicRouteProps = {
  page: PageDefinition;
};

export const PublicRoute: React.FC<PublicRouteProps> = ({ page }) => {
  const { isAuthenticated } = useAtomValue(userAtom);

  if (isAuthenticated && page === pages.LogIn) {
    return <Navigate to={pages.Home.paths[0]} />;
  }

  return <page.component />;
};
