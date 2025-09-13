import React from 'react';
import { Pages } from '~/navigation/pages';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { Button } from '~/sharedComponents/Button';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';
import { StyledLink } from '~/sharedComponents/StyledLink';

export const Register: React.FC = () => {
  return (
    <div className="content-background-color-primary">
      <div className="container mx-auto flex flex-col justify-center items-center py-10">
        <form className="w-full max-w-xs mb-12">
          <div className="mb-6">
            <FormLabel htmlFor="email">Email</FormLabel>

            <FormTextInput id="email" name="email" type="email" />
          </div>

          <div className="mb-6">
            <FormLabel htmlFor="display-name">Display name</FormLabel>

            <FormTextInput id="display-name" name="display-name" type="text" />
          </div>

          <div className="mb-6">
            <FormLabel htmlFor="password">Passowrd</FormLabel>

            <FormTextInput id="passowrd" name="passowrd" type="password" />
          </div>

          <Button className="w-full" onClick={() => {}}>
            Register
          </Button>
        </form>

        <div>
          Already have an account? <StyledLink to={Pages.LogIn.paths[0]}>Log In</StyledLink>
        </div>
      </div>
    </div>
  );
};
