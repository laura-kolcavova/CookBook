import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { BrowserRouter } from 'react-router-dom';
import { Layout } from './pages/shared/Layout';
import { LocalizationProvider } from './localization/LocalizationProvider';
import { ErrorBoundary } from 'react-error-boundary';
import { ModalProvider } from './modals/ModalProvider';
import { ErrorFallback } from './pages/shared/ErrorFallback';
import { CurrentUserProvider } from './authentication/CurrentUserProvider';

const queryClient = new QueryClient();

export const App: React.FC = () => {
  return (
    <BrowserRouter>
      <LocalizationProvider>
        <ErrorBoundary FallbackComponent={ErrorFallback}>
          <QueryClientProvider client={queryClient}>
            <CurrentUserProvider>
              <ModalProvider>
                <Layout />
              </ModalProvider>
            </CurrentUserProvider>
          </QueryClientProvider>
        </ErrorBoundary>
      </LocalizationProvider>
    </BrowserRouter>
  );
};
