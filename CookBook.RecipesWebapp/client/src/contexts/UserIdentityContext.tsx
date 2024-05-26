import React, { PropsWithChildren, useState } from 'react';
import { EMPTY_USER, User } from 'src/models/accounts/user';

interface IUserIdentityContext {
  user: User;
}

export const UserIdentityContext = React.createContext<IUserIdentityContext>(
  {} as IUserIdentityContext,
);

export const UserIdentityContextProvider = ({ children }: PropsWithChildren) => {
  const [user] = useState<User>(EMPTY_USER);

  return (
    <UserIdentityContext.Provider
      value={{
        user,
      }}>
      {children}
    </UserIdentityContext.Provider>
  );
};
