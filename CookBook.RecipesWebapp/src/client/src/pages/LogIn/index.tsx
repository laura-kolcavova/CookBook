import React, { useState } from 'react';

import { useUserIdentity } from '~/authentication/LoggedUserProvider';
import type { LoginData } from './models/LoginData';
import { useNavigate } from 'react-router-dom';
import { FormTextInput } from '../shared/forms/FormTextInput';
import { FormLabel } from '../shared/forms/FormLabel';
import { Button } from '../shared/Button';
import { StyledLink } from '../shared/StyledLink';
import { pages } from '~/navigation/pages';

const EMPTY_LOGIN_DATA: LoginData = {
  email: '',
  password: '',
};

export const LogIn = () => {
  const { login } = useUserIdentity();

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

          <div className="mb-12">
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
          Need an account? <StyledLink to={pages.Register.paths[0]}>Register</StyledLink>
        </div>
      </div>
    </div>
  );
};
