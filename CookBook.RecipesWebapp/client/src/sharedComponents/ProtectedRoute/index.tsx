import { PropsWithChildren, useContext } from 'react';
import React from 'react';
import { Navigate } from 'react-router-dom';

import { UserIdentityContext } from 'src/contexts/UserIdentityContext';

export const ProtectedRoute: React.FC<PropsWithChildren> = ({ children }) => {
  const { user } = useContext(UserIdentityContext);

  if (!user.isAuthenticated) {
    return <Navigate to="/login" />;
  }

  // eslint-disable-next-line react/jsx-no-useless-fragment
  return <>{children}</>;
};
