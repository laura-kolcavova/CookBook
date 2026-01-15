import { createContext, PropsWithChildren, useContext, useState } from 'react';

export type Locale = 'en-gb' | 'cs-cz';

// type RawDictionary = {
//   [key: string]: string;
// };

// const fetchDictionary = async (locale: Locale): Promise<RawDictionary> => {
//   const dictionary: RawDictionary = (await import(`~/i18n/${locale}.json`)).default;

//   return dictionary;
// };

export type LocalizationContextValue = {
  locale: string;
  // translate: (messageId: string) => string | undefined;
};

const LocalizationContext = createContext<LocalizationContextValue | null>(null);

type LocalizationProviderProps = PropsWithChildren;

export const LocalizationProvider = ({ children }: LocalizationProviderProps) => {
  const [locale] = useState<Locale>('cs-cz');

  // const [data] = createResource(getLocale, fetchDictionary);

  // const translate = (messageId: string): string | undefined => {
  //   return i18n.translator(data)(messageId);
  // };

  return (
    <LocalizationContext.Provider value={{ locale }}>
      {children}
      {/* <Show when={!data.loading}>{props.children}</Show> */}
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
