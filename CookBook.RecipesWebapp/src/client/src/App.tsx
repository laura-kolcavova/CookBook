import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './pages/shared/Layout';
import { LocalizationProvider } from './localization/LocalizationProvider';
import { ErrorBoundary } from 'react-error-boundary';
import { ErrorFallback } from './pages/ErrorFallback';
import { LoggedUserProvider } from './authentication/LoggedUserProvider';
import { ModalProvider } from './modals/ModalProvider';

const queryClient = new QueryClient();

export const App: React.FC = () => {
  return (
    <BrowserRouter>
      <LocalizationProvider>
        <ErrorBoundary FallbackComponent={ErrorFallback}>
          <QueryClientProvider client={queryClient}>
            <LoggedUserProvider>
              <ModalProvider>
                <Layout />
              </ModalProvider>
            </LoggedUserProvider>
          </QueryClientProvider>
        </ErrorBoundary>
      </LocalizationProvider>
    </BrowserRouter>
  );
};
