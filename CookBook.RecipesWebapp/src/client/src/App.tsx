import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { UserIdentityProvider } from './authentication/UserIdentityProvider';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './pages/shared/Layout';
import { LocalizationProvider } from './localization/LocalizationProvider';
import { ErrorBoundary } from 'react-error-boundary';
import { ErrorFallback } from './pages/ErrorFallback';

const queryClient = new QueryClient();

export const App: React.FC = () => {
  return (
    <LocalizationProvider>
      <QueryClientProvider client={queryClient}>
        <UserIdentityProvider>
          <BrowserRouter>
            <ErrorBoundary FallbackComponent={ErrorFallback}>
              <Layout />
            </ErrorBoundary>
          </BrowserRouter>
        </UserIdentityProvider>
      </QueryClientProvider>
    </LocalizationProvider>
  );
};
