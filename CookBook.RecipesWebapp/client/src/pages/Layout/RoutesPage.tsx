import React from 'react';

import { Route, Routes } from 'react-router-dom';
import { Pages } from '../../navigation/pages';
import { IPage } from 'src/navigation/models';
import { ProtectedRoute } from 'src/sharedComponents/ProtectedRoute';

export const RoutesPage: React.FC = () => {
  const getRouteElement = (page: IPage) => {
    const element = <page.component />;

    return page.public ? element : <ProtectedRoute>{element}</ProtectedRoute>;
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

