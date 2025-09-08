import React from 'react';

import { Header } from './Header';
import { Main } from './Main';
import { Footer } from './Footer';

export const Layout: React.FC = () => {
  return (
    <>
      <Header />
      <Main />
      <Footer />
    </>
  );
};
