import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ErrorBoundary } from 'react-error-boundary';
import { BrowserRouter } from 'react-router-dom';
import { CurrentUserProvider } from './authentication/CurrentUserProvider';
import { LocalizationProvider } from './localization/LocalizationProvider';
import { ModalProvider } from './modals/ModalProvider';
import { ErrorFallback } from './pages/shared/ErrorFallback';
import { Layout } from './pages/shared/Layout';

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
