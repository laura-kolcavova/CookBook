import React from 'react';

import { RoutesPage } from './RoutesPage';
import { Header } from './Header';
import { Container } from 'reactstrap';

export const Layout: React.FC = () => {
  return (
    <>
      <Header />

      <main>
        <div className="mb-3" id="content">
          <Container className="my-md-4" fluid="xl">
            <RoutesPage />
          </Container>
        </div>
      </main>

      <footer></footer>
    </>
  );
};
