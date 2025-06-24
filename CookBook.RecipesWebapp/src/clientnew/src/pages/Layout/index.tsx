import React from 'react';

import { RoutesPage } from './RoutesPage';
import { Header } from './Header';

export const Layout: React.FC = () => {
  return (
    <>
      <Header />
      <div className="page-wrapper">
        <div className="container content-wrapper">
          <main id="content">
            <RoutesPage />
          </main>
        </div>
      </div>
    </>
  );
};

