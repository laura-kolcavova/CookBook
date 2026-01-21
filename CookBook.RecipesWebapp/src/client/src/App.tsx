import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './pages/shared/Layout';
import { LocalizationProvider } from './localization/LocalizationProvider';
import { ErrorBoundary } from 'react-error-boundary';
import { ErrorFallback } from './pages/ErrorFallback';
import { LoggedUserProvider } from './authentication/LoggedUserProvider';

const queryClient = new QueryClient();

export const App: React.FC = () => {
  return (
    <LocalizationProvider>
      <QueryClientProvider client={queryClient}>
        <LoggedUserProvider>
          <BrowserRouter>
            <ErrorBoundary FallbackComponent={ErrorFallback}>
              <Layout />
            </ErrorBoundary>
          </BrowserRouter>
        </LoggedUserProvider>
      </QueryClientProvider>
    </LocalizationProvider>
  );
};
