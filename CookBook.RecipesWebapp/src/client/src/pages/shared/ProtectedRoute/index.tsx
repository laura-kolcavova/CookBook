import React from 'react';
import { useAtomValue } from 'jotai';
import { Navigate } from 'react-router-dom';
import { userAtom } from '~/atoms/userAtom';

import { Pages } from '~/navigation/pages';
import type { PageDefinition } from '~/navigation/models';

export type ProtectedRouteProps = {
  page: PageDefinition;
};

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ page }) => {
  const { isAuthenticated } = useAtomValue(userAtom);

  if (!isAuthenticated) {
    return <Navigate to={Pages.LogIn.paths[0]} />;
  }

  return <page.component />;
};
