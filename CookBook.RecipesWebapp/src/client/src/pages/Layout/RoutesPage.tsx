import React from 'react';

import { Route, Routes } from 'react-router-dom';
import { IPage } from 'src/navigation/models';
import { Pages } from 'src/navigation/pages';
import { ProtectedRoute } from 'src/sharedComponents/ProtectedRoute';
import { PublicRoute } from 'src/sharedComponents/PublicRoute';

export const RoutesPage: React.FC = () => {
  const getRouteElement = (page: IPage) => {
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
