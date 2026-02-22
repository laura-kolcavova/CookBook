import React from 'react';

import { Route, Routes } from 'react-router-dom';

import type { PageDefinition } from '~/navigation/PageDefinition';
import { pages } from '~/navigation/pages';
import { ProtectedRoute } from '~/pages/shared/routes/ProtectedRoute';

export const RoutesPage: React.FC = () => {
  const getRouteElement = (page: PageDefinition) => {
    return page.public ? <page.component /> : <ProtectedRoute page={page} />;
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
