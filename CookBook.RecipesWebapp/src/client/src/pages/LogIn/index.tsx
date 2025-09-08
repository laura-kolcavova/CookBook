import React, { useContext, useState } from 'react';
import { Content } from './styled';
import { Link } from 'react-router-dom';

import { useRouter } from '../../navigation/hooks/useRouter';

import { Pages } from '../../navigation/pages';

import { UserIdentityContext } from '~/contexts/UserIdentityContext';
import { Form } from '~/sharedComponents/forms/Form';
import { FormGroup } from '~/sharedComponents/forms/FormGroup';
import { Label } from '~/sharedComponents/forms/Label';
import { Input } from '~/sharedComponents/forms/Input';
import { Button } from '~/sharedComponents/forms/Button';
import type { LoginData } from './models/LoginData';

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
    <Content className="h-full">
      <div className="container mx-auto">
        <div className="flex flex-col justify-center items-center">
          <Form>
            <FormGroup>
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                name="email"
                type="email"
                value={data.email}
                onChange={handleEmailChange}
                autoComplete="email"
              />
            </FormGroup>

            <FormGroup>
              <Label htmlFor="password">Passowrd</Label>
              <Input
                id="passowrd"
                name="passowrd"
                type="password"
                value={data.password}
                onChange={handlePasswordChange}
                autoComplete="current-password"
              />
            </FormGroup>

            <Button onClick={handleSubmit}>Log In</Button>
          </Form>

          <div>
            Need an account? <Link to={Pages.Register.paths[0]}>Register</Link>
          </div>
        </div>
      </div>
    </Content>
  );
};
