import React from 'react';
import { useAtomValue } from 'jotai';
import { Navigate } from 'react-router-dom';
import { userAtom } from '~/atoms/userAtom';

import { IPage } from '~/navigation/models';
import { Pages } from '~/navigation/pages';

export type ProtectedRouteProps = {
  page: IPage;
};

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ page }) => {
  const { isAuthenticated } = useAtomValue(userAtom);

  if (!isAuthenticated) {
    return <Navigate to={Pages.LogIn.paths[0]} />;
  }

  return <page.component />;
};
