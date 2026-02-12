import { FormLabel } from '../shared/forms/FormLabel';
import { FormTextInput } from '../shared/forms/FormTextInput';
import { Button } from '../shared/Button';
import { StyledLink } from '../shared/StyledLink';
import { pages } from '~/navigation/pages';
import { FormattedMessage } from 'react-intl';
import { messages } from './messages';
import { useRegisterUserMutation } from './hooks/useRegisterUserMutation';
import { useRegisterUserErrorMessage } from './hooks/useRegisterUserErrorMessage';
import { Alert } from '../shared/Alert';
import { useEffect, useState } from 'react';
import type { RegisterUserRequestDto } from '~/api/users/dto/RegisterUserRequestDto';
import { SpinnerIcon } from '../shared/icons/SpinnerIcon';
import { useNavigate } from 'react-router-dom';

export const Register = () => {
  const navigate = useNavigate();

  const [displayName, setDisplayName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const {
    mutate: registerUseMutate,
    isPending: registerUserIsPending,
    isSuccess: registerUserIsSuccess,
    isError: registerUserIsError,
    error: registerUserError,
  } = useRegisterUserMutation();

  const { getErrorMessage } = useRegisterUserErrorMessage();

  useEffect(() => {
    if (registerUserIsSuccess) {
      navigate(pages.Home.paths[0]);
    }
  }, [navigate, registerUserIsSuccess]);

  const handleDisplayNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayName(e.target.value);
  };

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const handleSubmit = (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    const registerUserRequest: RegisterUserRequestDto = {
      displayName: displayName,
      email: email,
      password: password,
    };

    registerUseMutate(registerUserRequest);
  };

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        {registerUserIsError && (
          <Alert color="danger" isDismissible={true}>
            {getErrorMessage(registerUserError!)}
          </Alert>
        )}

        <form className="w-full max-w-xs mx-auto mb-12" onSubmit={handleSubmit}>
          <div className="mb-6">
            <FormLabel htmlFor="display-name">
              <FormattedMessage {...messages.displayNameLabel} />
            </FormLabel>

            <FormTextInput
              id="display-name"
              name="display-name"
              type="text"
              autoComplete="off"
              required
              maxLength={256}
              onChange={handleDisplayNameChange}
            />
          </div>

          <div className="mb-6">
            <FormLabel htmlFor="email">
              <FormattedMessage {...messages.emailLabel} />
            </FormLabel>

            <FormTextInput
              id="email"
              name="email"
              type="email"
              autoComplete="email"
              required
              maxLength={256}
              onChange={handleEmailChange}
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
              autoComplete="off"
              required
              maxLength={256}
              onChange={handlePasswordChange}
            />
          </div>

          <Button
            type="submit"
            variant="primary"
            className="w-full flex items-center justify-center"
            disabled={registerUserIsPending}>
            <span>
              <FormattedMessage {...messages.registerButton} />
            </span>

            {registerUserIsPending && <SpinnerIcon className="animate-spin size-4 ml-2" />}
          </Button>
        </form>

        <div className="text-center">
          <FormattedMessage {...messages.alreadyHaveAccount} />{' '}
          <StyledLink to={pages.LogIn.paths[0]}>
            <FormattedMessage {...messages.logInLink} />
          </StyledLink>
        </div>
      </div>
    </div>
  );
};
