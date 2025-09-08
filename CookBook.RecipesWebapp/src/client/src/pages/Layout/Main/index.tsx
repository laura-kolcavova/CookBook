import React from 'react';

import { RoutesPage } from './RoutesPage';

export const Main: React.FC = () => {
  return (
    <main className="flex-auto">
      <div className="h-full pt-6 pb-6">
        <RoutesPage />
      </div>
    </main>
  );
};
