import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
  type PropsWithChildren,
} from 'react';

import type { CurrentUserDto } from '~/api/users/dto/CurrentUserDto';
import { useGetCurrentUserQuery } from './hooks/useGetCurrentUserQuery';
import { SpinnerIcon } from '~/pages/shared/icons/SpinnerIcon';

export type CurrentUserContextValue = {
  currentUser: CurrentUserDto;
  resetCurrentUser: () => void;
  refreshCurrentUser: () => void;
};

export const ANONYMOUS_USER: CurrentUserDto = {
  isAuthenticated: false,
  userName: '',
  displayName: '',
  email: '',
};

const CurrentUserContext = createContext<CurrentUserContextValue | null>(null);

export type CurrentUserProviderProps = PropsWithChildren;

export const CurrentUserProvider = ({ children }: CurrentUserProviderProps) => {
  const [currentUser, setCurrentUser] = useState<CurrentUserDto>(ANONYMOUS_USER);
  const [isReady, setIsReady] = useState<boolean>(false);

  const { data, isSuccess, refetch } = useGetCurrentUserQuery();

  const resetCurrentUser = useCallback(() => {
    setCurrentUser(ANONYMOUS_USER);
  }, []);

  const refreshCurrentUser = useCallback(() => {
    setIsReady(false);
    refetch();
  }, [refetch]);

  useEffect(() => {
    if (isSuccess && data) {
      // eslint-disable-next-line react-hooks/set-state-in-effect
      setCurrentUser(data);
      setIsReady(true);
    }
  }, [data, isSuccess]);

  return (
    <CurrentUserContext.Provider
      value={{
        currentUser,
        resetCurrentUser,
        refreshCurrentUser,
      }}>
      {isReady ? (
        children
      ) : (
        <div className="flex flex-col justify-center items-center h-full">
          <SpinnerIcon className="animate-spin size-5 mx-auto" />
        </div>
      )}
    </CurrentUserContext.Provider>
  );
};

export const useCurrentUser = () => {
  const contextValue = useContext(CurrentUserContext);

  if (contextValue === null) {
    throw new Error('CurrentUserProvider missing');
  }

  return contextValue;
};
