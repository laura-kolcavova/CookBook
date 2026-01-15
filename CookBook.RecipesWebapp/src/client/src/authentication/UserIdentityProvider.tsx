import { useSetAtom } from 'jotai';
import { useResetAtom } from 'jotai/utils';
import { createContext, useContext, type PropsWithChildren } from 'react';

import { userAtom } from '~/atoms/userAtom';

export type UserIdentityContextValue = {
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
};

const UserIdentityContext = createContext<UserIdentityContextValue | null>(null);

export const UserIdentityProvider = ({ children }: PropsWithChildren) => {
  const setUser = useSetAtom(userAtom);
  const resetUser = useResetAtom(userAtom);

  const login = (email: string, pasword: string): Promise<void> => {
    console.log(email, pasword);

    setUser({
      isAuthenticated: true,
      nameClaimType: 'Test Name',
      emailClaimType: 'test@cookbook.com',
      roleClaimType: 'admin',
      claims: [],
    });

    return Promise.resolve();
  };

  const logout = () => {
    resetUser();
  };

  return (
    <UserIdentityContext.Provider
      value={{
        login,
        logout,
      }}>
      {children}
    </UserIdentityContext.Provider>
  );
};

export const useUserIdentity = () => {
  const contextValue = useContext(UserIdentityContext);

  if (contextValue === null) {
    throw new Error('UserIdentityProvider missing');
  }

  return contextValue;
};
