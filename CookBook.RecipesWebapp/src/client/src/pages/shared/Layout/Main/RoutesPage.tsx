import React from 'react';

import { Route, Routes } from 'react-router-dom';

import { PublicRoute } from '../../PublicRoute';
import { ProtectedRoute } from '../../ProtectedRoute';
import { PageDefinition } from '~/navigation/PageDefinition';
import { pages } from '~/navigation/pages';

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
