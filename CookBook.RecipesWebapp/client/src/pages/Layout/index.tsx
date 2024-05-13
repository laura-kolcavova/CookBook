import React from 'react';

import { RoutesPage } from './RoutesPage';

export const Layout: React.FC = () => {
  return (
    <div className="page-wrapper">
      <div className="content-wrapper">
        <div id="content">
          <RoutesPage />
        </div>
      </div>
    </div>
  );
};
