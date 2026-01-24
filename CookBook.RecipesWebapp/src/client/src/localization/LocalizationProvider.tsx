import type { PropsWithChildren } from 'react';
import { createContext, useContext, useMemo, useState } from 'react';
import type { Locale } from './Locale';
import { IntlProvider } from 'react-intl';

import messages_en_gb from '~/i18n/en-gb.json';

export type LocalizationContextValue = {
  locale: string;
};

const LocalizationContext = createContext<LocalizationContextValue | null>(null);

type LocalizationProviderProps = PropsWithChildren;

export const LocalizationProvider = ({ children }: LocalizationProviderProps) => {
  const [locale] = useState<Locale>('en-gb');

  const messages = useMemo(() => {
    switch (locale) {
      case 'en-gb':
        return messages_en_gb;
      default:
        return messages_en_gb;
    }
  }, [locale]);

  return (
    <LocalizationContext.Provider value={{ locale }}>
      <IntlProvider locale={locale} messages={messages}>
        {children}
      </IntlProvider>
    </LocalizationContext.Provider>
  );
};

export const useLocalization = () => {
  const contextValue = useContext(LocalizationContext);

  if (contextValue === null) {
    throw new Error('LocalizationProvider missing');
  }

  return contextValue;
};
