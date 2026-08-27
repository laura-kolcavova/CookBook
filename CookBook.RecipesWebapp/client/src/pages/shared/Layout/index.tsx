import React from 'react';

import { ToastContainer } from 'react-toastify';
import { Footer } from './shared/Footer';
import { Header } from './shared/Header';
import { Main } from './shared/Main';

export const Layout: React.FC = () => {
  return (
    <>
      <Header />
      <Main />
      <Footer />
      <ToastContainer />
    </>
  );
};
