import React from 'react';
import { FormWrapper } from './styled';
import { Form, FormGroup, Input, Label } from 'reactstrap';

export const LogIn: React.FC = () => {
  return (
    <FormWrapper>
      <Form>
        <FormGroup>
          <Label for="email">Email</Label>
          <Input id="email" name="email" type="email" />
        </FormGroup>

        <FormGroup>
          <Label for="password">Passowrd</Label>
          <Input id="passowrd" name="passowrd" type="password" />
        </FormGroup>
      </Form>
    </FormWrapper>
  );
};
