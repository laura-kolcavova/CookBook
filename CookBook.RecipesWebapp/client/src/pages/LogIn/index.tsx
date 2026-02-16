import React, { useEffect, useState } from 'react';
import { FormattedMessage } from 'react-intl';

import type { LoginData } from './models/LoginData';
import { useNavigate } from 'react-router-dom';
import { FormTextInput } from '../shared/forms/FormTextInput';
import { FormLabel } from '../shared/forms/FormLabel';
import { Button } from '../shared/Button';
import { StyledLink } from '../shared/StyledLink';
import { pages } from '~/navigation/pages';
import { messages } from './messages';
import { useLogInUserMutation } from './hooks/useLogInUserMutation';
import type { LogInUserRequestDto } from '~/api/users/dto/LogInUserRequestDto';
import { SpinnerIcon } from '../shared/icons/SpinnerIcon';
import { Alert } from '../shared/Alert';
import { useLogInUserErrorMessage } from './hooks/useLogInErrorMessage';

const EMPTY_LOGIN_DATA: LoginData = {
  email: '',
  password: '',
};

export const LogIn = () => {
  const navigate = useNavigate();

  const [data, setData] = useState<LoginData>(EMPTY_LOGIN_DATA);

  const {
    mutate: logInUserMutate,
    isPending: logInUserIsPending,
    isSuccess: logInUserIsSuccess,
    isError: logInUserIsError,
    error: logInUserError,
  } = useLogInUserMutation();

  const { getErrorMessage } = useLogInUserErrorMessage();

  useEffect(() => {
    if (logInUserIsSuccess) {
      navigate(pages.Home.paths[0]);
    }
  }, [logInUserIsSuccess, navigate]);

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setData({ ...data, email: e.target.value });
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setData({ ...data, password: e.target.value });
  };

  const handleSubmit = async (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    const logInUserRequest: LogInUserRequestDto = {
      email: data.email,
      password: data.password,
    };

    logInUserMutate(logInUserRequest);
  };

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        {logInUserIsError && (
          <Alert color="danger" isDismissible={true}>
            {getErrorMessage(logInUserError!)}
          </Alert>
        )}

        <form className="w-full max-w-xs mx-auto mb-12" onSubmit={handleSubmit}>
          <div className="mb-6">
            <FormLabel htmlFor="email">
              <FormattedMessage {...messages.emailLabel} />
            </FormLabel>

            <FormTextInput
              id="email"
              name="email"
              type="email"
              value={data.email}
              onChange={handleEmailChange}
              autoComplete="email"
            />
          </div>

          <div className="mb-12">
            <FormLabel htmlFor="password">
              <FormattedMessage {...messages.passwordLabel} />
            </FormLabel>

            <FormTextInput
              id="passowrd"
              name="passowrd"
              type="password"
              value={data.password}
              onChange={handlePasswordChange}
              autoComplete="current-password"
            />
          </div>

          <Button
            type="submit"
            variant="primary"
            className="w-full flex items-center justify-center"
            disabled={logInUserIsPending}>
            <span>
              <FormattedMessage {...messages.logInButton} />
            </span>

            {logInUserIsPending && <SpinnerIcon className="animate-spin size-4 ml-2" />}
          </Button>
        </form>

        <div className="text-center">
          <FormattedMessage {...messages.needAccount} />{' '}
          <StyledLink to={pages.Register.paths[0]}>
            <FormattedMessage {...messages.registerLink} />
          </StyledLink>
        </div>
      </div>
    </div>
  );
};
