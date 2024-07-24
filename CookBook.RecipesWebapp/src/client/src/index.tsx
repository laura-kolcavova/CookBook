import React from 'react';
import { BrowserRouter } from 'react-router-dom';

import ReactDOM from 'react-dom/client';

import './index.css';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import GlobalStyle from './globalStyles';
import { Layout } from './pages/Layout';

import 'bootstrap/dist/css/bootstrap.min.css';
import { UserIdentityContextProvider } from './contexts/UserIdentityContext';

const queryClient = new QueryClient();
const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
  <QueryClientProvider client={queryClient}>
    <GlobalStyle />
    <UserIdentityContextProvider>
      <BrowserRouter>
        <Layout />
      </BrowserRouter>
    </UserIdentityContextProvider>
  </QueryClientProvider>,
);

