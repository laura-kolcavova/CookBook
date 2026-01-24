import React from 'react';

import { Route, Routes } from 'react-router-dom';

import type { PageDefinition } from '~/navigation/PageDefinition';
import { pages } from '~/navigation/pages';
import { ProtectedRoute } from '~/pages/shared/routes/ProtectedRoute';
import { PublicRoute } from '~/pages/shared/routes/PublicRoute';

export const RoutesPage: React.FC = () => {
  const getRouteElement = (page: PageDefinition) => {
    return page.public ? <PublicRoute page={page} /> : <ProtectedRoute page={page} />;
  };

  return (
    <Routes>
      {Object.values(pages).map((page, labelNumber) => {
        return page.paths.map((path) => (
          <Route key={`route-${labelNumber}`} path={path} element={getRouteElement(page)} />
        ));
      })}
    </Routes>
  );
};
