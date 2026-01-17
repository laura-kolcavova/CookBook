import React from 'react';
import { FormLabel } from '../shared/forms/FormLabel';
import { FormTextInput } from '../shared/forms/FormTextInput';
import { Button } from '../shared/Button';
import { StyledLink } from '../shared/StyledLink';
import { pages } from '~/navigation/pages';

export const Register: React.FC = () => {
  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4 flex flex-col items-center justify-center">
        <form className="w-full max-w-xs mb-12">
          <div className="mb-6">
            <FormLabel htmlFor="email">Email</FormLabel>

            <FormTextInput id="email" name="email" type="email" />
          </div>

          <div className="mb-6">
            <FormLabel htmlFor="display-name">Display name</FormLabel>

            <FormTextInput id="display-name" name="display-name" type="text" />
          </div>

          <div className="mb-12">
            <FormLabel htmlFor="password">Passowrd</FormLabel>

            <FormTextInput id="passowrd" name="passowrd" type="password" />
          </div>

          <Button className="w-full" onClick={() => {}}>
            Register
          </Button>
        </form>

        <div>
          Already have an account? <StyledLink to={pages.LogIn.paths[0]}>Log In</StyledLink>
        </div>
      </div>
    </div>
  );
};
