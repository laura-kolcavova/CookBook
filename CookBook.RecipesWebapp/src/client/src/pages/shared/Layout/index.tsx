import React from 'react';

import { Footer } from './shared/Footer';
import { Main } from './shared/Main';
import { Header } from './shared/Header';

export const Layout: React.FC = () => {
  return (
    <>
      <Header />
      <Main />
      <Footer />
    </>
  );
};
