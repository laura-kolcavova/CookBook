import React from 'react';
import { FormWrapper } from './styled';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import { Link } from 'react-router-dom';
import { Pages } from 'src/navigation/pages';

export const Register: React.FC = () => {
  return (
    <FormWrapper>
      <Form>
        <FormGroup>
          <Label for="email">Email</Label>
          <Input id="email" name="email" type="email" />
        </FormGroup>

        <FormGroup>
          <Label for="display-name">Display name</Label>
          <Input id="display-name" name="display-name" type="text" />
        </FormGroup>

        <FormGroup>
          <Label for="password">Passowrd</Label>
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
