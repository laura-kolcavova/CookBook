import React from 'react';
import { FormWrapper } from './styled';
import { Form, Link } from 'react-router-dom';
import { Pages } from '~/navigation/pages';
import { FormGroup } from '~/sharedComponents/forms/FormGroup';
import { Label } from '~/sharedComponents/forms/Label';
import { Button } from '~/sharedComponents/forms/Button';
import { Input } from '~/sharedComponents/forms/Input';

export const Register: React.FC = () => {
  return (
    <FormWrapper>
      <Form>
        <FormGroup>
          <Label htmlFor="email">Email</Label>
          <Input id="email" name="email" type="email" />
        </FormGroup>

        <FormGroup>
          <Label htmlFor="display-name">Display name</Label>
          <Input id="display-name" name="display-name" type="text" />
        </FormGroup>

        <FormGroup>
          <Label htmlFor="password">Passowrd</Label>
          <Input id="passowrd" name="passowrd" type="password" />
        </FormGroup>

        <Button onClick={() => {}}>Register</Button>
      </Form>

      <div>
        Already have an account? <Link to={Pages.LogIn.paths[0]}>Log In</Link>
      </div>
    </FormWrapper>
  );
};
