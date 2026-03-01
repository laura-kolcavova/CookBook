import { useEffect } from 'react';
import { useLogInUserMutation } from './hooks/useLogInUserMutation';
import { Alert } from '../shared/Alert';
import { useLogInUserErrorMessage } from './hooks/useLogInErrorMessage';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';

export const LogIn = () => {
  const { refreshCurrentUser } = useCurrentUser();

  const {
    mutate: logInUserMutate,
    isSuccess: logInUserIsSuccess,
    isError: logInUserIsError,
    error: logInUserError,
  } = useLogInUserMutation();

  const { getErrorMessage } = useLogInUserErrorMessage();

  useEffect(() => {
    if (logInUserIsSuccess) {
      refreshCurrentUser();
    }
  }, [logInUserIsSuccess, refreshCurrentUser]);

  useEffect(() => {
    logInUserMutate();
  }, [logInUserMutate]);

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        {logInUserIsError && (
          <Alert color="danger" isDismissible={true}>
            {getErrorMessage(logInUserError!)}
          </Alert>
        )}
      </div>
    </div>
  );
};
