import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { UserIdentityContextProvider } from './contexts/UserIdentityContext';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './pages/Layout';

const queryClient = new QueryClient();

export const App: React.FC = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <UserIdentityContextProvider>
        <BrowserRouter>
          <Layout />
        </BrowserRouter>
      </UserIdentityContextProvider>
    </QueryClientProvider>
  );
};
