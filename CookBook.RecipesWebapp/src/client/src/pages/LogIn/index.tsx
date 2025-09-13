import React, { useContext, useState } from 'react';

import { useRouter } from '../../navigation/hooks/useRouter';

import { Pages } from '../../navigation/pages';

import { UserIdentityContext } from '~/contexts/UserIdentityContext';

import { Button } from '~/sharedComponents/Button';
import type { LoginData } from './models/LoginData';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';
import { StyledLink } from '~/sharedComponents/StyledLink';

const EMPTY_LOGIN_DATA: LoginData = {
  email: '',
  password: '',
};

export const LogIn: React.FC = () => {
  const { login } = useContext(UserIdentityContext);

  const { goToPage } = useRouter();

  const [data, setData] = useState<LoginData>(EMPTY_LOGIN_DATA);

  const handleSubmit = async () => {
    await login(data.email, data.password);
    goToPage(Pages.Home);
  };

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setData({ ...data, email: e.target.value });
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setData({ ...data, password: e.target.value });
  };

  return (
    <div className="content-background-color-primary">
      <div className="container mx-auto flex flex-col justify-center items-center py-20">
        <form className="mb-12">
          <div className="mb-6">
            <FormLabel htmlFor="email">Email</FormLabel>

            <FormTextInput
              id="email"
              name="email"
              type="email"
              value={data.email}
              onChange={handleEmailChange}
              autoComplete="email"
            />
          </div>

          <div className="mb-6">
            <FormLabel htmlFor="password">Passowrd</FormLabel>

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
              Log In
            </Button>
          </div>
        </form>

        <div>
          Need an account? <StyledLink to={Pages.Register.paths[0]}>Register</StyledLink>
        </div>
      </div>
    </div>
  );
};
