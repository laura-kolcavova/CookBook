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

export const Register = () => {
  const {
    mutate: registerUseMutate,
    isPending: registerUserIsPending,
    isSuccess: registerUserIsSuccess,
    isError: registerUserIsError,
    error: registerUserError,
  } = useRegisterUserMutation();

  const { getErrorMessage } = useRegisterUserErrorMessage();

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4 flex flex-col items-center justify-center">
        {registerUserIsError && (
          <Alert color="danger" isDismissible={true}>
            {getErrorMessage(registerUserError!)}
          </Alert>
        )}

        <form className="w-full max-w-xs mb-12">
          <div className="mb-6">
            <FormLabel htmlFor="email">
              <FormattedMessage {...messages.emailLabel} />
            </FormLabel>

            <FormTextInput id="email" name="email" type="email" />
          </div>

          <div className="mb-6">
            <FormLabel htmlFor="display-name">
              <FormattedMessage {...messages.displayNameLabel} />
            </FormLabel>

            <FormTextInput id="display-name" name="display-name" type="text" />
          </div>

          <div className="mb-12">
            <FormLabel htmlFor="password">
              <FormattedMessage {...messages.passwordLabel} />
            </FormLabel>

            <FormTextInput id="passowrd" name="passowrd" type="password" />
          </div>

          <Button className="w-full" onClick={() => {}}>
            <FormattedMessage {...messages.registerButton} />
          </Button>
        </form>

        <div>
          <FormattedMessage {...messages.alreadyHaveAccount} />{' '}
          <StyledLink to={pages.LogIn.paths[0]}>
            <FormattedMessage {...messages.logInLink} />
          </StyledLink>
        </div>
      </div>
    </div>
  );
};
