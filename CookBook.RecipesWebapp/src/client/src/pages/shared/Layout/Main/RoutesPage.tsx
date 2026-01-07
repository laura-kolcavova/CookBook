import React from 'react';

import { Route, Routes } from 'react-router-dom';
import type { PageDefinition } from '~/navigation/models';

import { Pages } from '~/navigation/pages';
import { ProtectedRoute } from '~/sharedComponents/ProtectedRoute';
import { PublicRoute } from '~/sharedComponents/PublicRoute';

export const RoutesPage: React.FC = () => {
  const getRouteElement = (page: PageDefinition) => {
    return page.public ? <PublicRoute page={page} /> : <ProtectedRoute page={page} />;
  };

  return (
    <Routes>
      {Object.values(Pages).map((page, labelNumber) => {
        return page.paths.map((path) => (
          <Route key={`route-${labelNumber}`} path={path} element={getRouteElement(page)} />
        ));
      })}
    </Routes>
  );
};
