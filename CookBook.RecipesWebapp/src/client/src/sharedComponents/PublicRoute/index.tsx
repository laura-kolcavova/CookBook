import { useAtomValue } from 'jotai';
import React from 'react';
import { Navigate } from 'react-router-dom';
import { userAtom } from '~/atoms/userAtom';

import { IPage } from '~/navigation/models';
import { Pages } from '~/navigation/pages';

export type PublicRouteProps = {
  page: IPage;
};

export const PublicRoute: React.FC<PublicRouteProps> = ({ page }) => {
  const { isAuthenticated } = useAtomValue(userAtom);

  if (isAuthenticated && page === Pages.LogIn) {
    return <Navigate to={Pages.Home.paths[0]} />;
  }

  return <page.component />;
};
