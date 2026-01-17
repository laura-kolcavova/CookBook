import React from 'react';
import { useAtomValue } from 'jotai';
import { Navigate } from 'react-router-dom';
import { userAtom } from '~/atoms/userAtom';
import { PageDefinition } from '~/navigation/PageDefinition';
import { pages } from '~/navigation/pages';

export type ProtectedRouteProps = {
  page: PageDefinition;
};

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ page }) => {
  const { isAuthenticated } = useAtomValue(userAtom);

  if (!isAuthenticated) {
    return <Navigate to={pages.LogIn.paths[0]} />;
  }

  return <page.component />;
};
