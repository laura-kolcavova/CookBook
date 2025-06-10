import React, { useContext, useState } from 'react';
import { FormWrapper } from './styled';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import { Link } from 'react-router-dom';

import { useRouter } from '../../navigation/hooks/useRouter';
import { UserIdentityContext } from '../../contexts/UserIdentityContext';
import { Pages } from '../../navigation/pages';
import { LoginData } from './models/LoginData';

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
    <FormWrapper>
      <Form>
        <FormGroup>
          <Label for="email">Email</Label>
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
          <Label for="password">Passowrd</Label>
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
    </FormWrapper>
  );
};
