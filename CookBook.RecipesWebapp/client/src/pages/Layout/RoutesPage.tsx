import React from 'react';

import { Route, Routes } from 'react-router-dom';
import { Pages } from '../../navigation/pages';

export const RoutesPage: React.FC = () => {
  return (
    <Routes>
      {Object.values(Pages).map((page, labelNumber) => {
        return page.paths.map((path) => (
          <Route key={`route-${labelNumber}`} path={path} element={<page.component />} />
        ));
      })}
    </Routes>
  );
};
