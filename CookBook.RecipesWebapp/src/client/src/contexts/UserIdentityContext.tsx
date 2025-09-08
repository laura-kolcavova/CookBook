import { useSetAtom } from 'jotai';
import { useResetAtom } from 'jotai/utils';
import { createContext, type PropsWithChildren } from 'react';

import { userAtom } from '~/atoms/userAtom';

interface IUserIdentityContext {
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
}

export const UserIdentityContext = createContext<IUserIdentityContext>({} as IUserIdentityContext);

export const UserIdentityContextProvider = ({ children }: PropsWithChildren) => {
  const setUser = useSetAtom(userAtom);
  const resetUser = useResetAtom(userAtom);

  const login = (_email: string, _pasword: string): Promise<void> => {
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
