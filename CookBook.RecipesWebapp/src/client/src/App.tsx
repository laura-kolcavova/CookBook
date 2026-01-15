import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { UserIdentityProvider } from './authentication/UserIdentityProvider';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './pages/shared/Layout';
import { LocalizationProvider } from './localization/LocalizationProvider';

const queryClient = new QueryClient();

export const App: React.FC = () => {
  return (
    <LocalizationProvider>
      <QueryClientProvider client={queryClient}>
        <UserIdentityProvider>
          <BrowserRouter>
            <Layout />
          </BrowserRouter>
        </UserIdentityProvider>
      </QueryClientProvider>
    </LocalizationProvider>
  );
};
