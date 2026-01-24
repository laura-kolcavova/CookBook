import React, { useState } from 'react';
import { FormattedMessage } from 'react-intl';

import type { LoginData } from './models/LoginData';
import { useNavigate } from 'react-router-dom';
import { FormTextInput } from '../shared/forms/FormTextInput';
import { FormLabel } from '../shared/forms/FormLabel';
import { Button } from '../shared/Button';
import { StyledLink } from '../shared/StyledLink';
import { pages } from '~/navigation/pages';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';
import { messages } from './messages';

const EMPTY_LOGIN_DATA: LoginData = {
  email: '',
  password: '',
};

export const LogIn = () => {
  const { login } = useLoggedUser();

  const navigate = useNavigate();

  const [data, setData] = useState<LoginData>(EMPTY_LOGIN_DATA);

  const handleSubmit = async () => {
    await login(data.email, data.password);

    navigate(pages.Home.paths[0]);
  };

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setData({ ...data, email: e.target.value });
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setData({ ...data, password: e.target.value });
  };

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4 flex flex-col items-center justify-center">
        <form className="w-full max-w-xs mb-12">
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

          <div>
            <Button className="w-full" onClick={handleSubmit}>
              <FormattedMessage {...messages.logInButton} />
            </Button>
          </div>
        </form>

        <div>
          <FormattedMessage {...messages.needAccount} />{' '}
          <StyledLink to={pages.Register.paths[0]}>
            <FormattedMessage {...messages.registerLink} />
          </StyledLink>
        </div>
      </div>
    </div>
  );
};
