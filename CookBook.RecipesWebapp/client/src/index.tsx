import React from 'react';
import { BrowserRouter } from 'react-router-dom';

import ReactDOM from 'react-dom/client';

import './index.css';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import GlobalStyle from './globalStyles';
import { Layout } from './pages/Layout';

const queryClient = new QueryClient();
const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
  <QueryClientProvider client={queryClient}>
    <GlobalStyle />
    <BrowserRouter>
      <Layout />
    </BrowserRouter>
  </QueryClientProvider>,
);
