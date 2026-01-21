import { useAtom } from 'jotai';
import { atomWithStorage, createJSONStorage, useResetAtom } from 'jotai/utils';
import { createContext, useContext, useState, type PropsWithChildren } from 'react';
import type { LoggedUser } from './models/LoggedUser';
import { LOCAL_STORAGE_LOGGED_USER } from '~/constants';

const loggedUserJsonStorage = createJSONStorage<LoggedUser>(() => localStorage);

const EMPTY_LOGGED_USER: LoggedUser = {
  fullName: '',
  firstName: '',
  lastName: '',
  userId: 0,
  roles: [],
};

const userAtom = atomWithStorage<LoggedUser>(
  LOCAL_STORAGE_LOGGED_USER,
  EMPTY_LOGGED_USER,
  loggedUserJsonStorage,
  {
    getOnInit: true,
  },
);

export type LoggedUserContextValue = {
  isAuthenticated: boolean;
  user: LoggedUser;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
};

const LoggedUserContext = createContext<LoggedUserContextValue | null>(null);

export const LoggedUserProvider = ({ children }: PropsWithChildren) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useAtom(userAtom);
  const resetUser = useResetAtom(userAtom);

  const login = (email: string, pasword: string): Promise<void> => {
    console.log(email, pasword);

    setUser({
      fullName: 'John Doe',
      firstName: 'John',
      lastName: 'Doe',
      userId: 1,
      roles: [],
    });

    setIsAuthenticated(true);

    return Promise.resolve();
  };

  const logout = () => {
    resetUser();
  };

  return (
    <LoggedUserContext.Provider
      value={{
        isAuthenticated,
        user,
        login,
        logout,
      }}>
      {children}
    </LoggedUserContext.Provider>
  );
};

export const useLoggedUser = () => {
  const contextValue = useContext(LoggedUserContext);

  if (contextValue === null) {
    throw new Error('LoggedUserProvider missing');
  }

  return contextValue;
};
