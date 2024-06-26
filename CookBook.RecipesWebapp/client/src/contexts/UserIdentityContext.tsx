import React, { PropsWithChildren, useState } from 'react';
import { EMPTY_USER, User } from 'src/models/accounts/user';

interface IUserIdentityContext {
  user: User;
  login: (email: string, password: string) => Promise<void>;
}

export const UserIdentityContext = React.createContext<IUserIdentityContext>(
  {} as IUserIdentityContext,
);

export const UserIdentityContextProvider = ({ children }: PropsWithChildren) => {
  const [user, setUser] = useState<User>(EMPTY_USER);

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

  return (
    <UserIdentityContext.Provider
      value={{
        user,
        login,
      }}>
      {children}
    </UserIdentityContext.Provider>
  );
};
