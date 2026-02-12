import { defineMessages } from 'react-intl';

export const messages = defineMessages({
  emailLabel: {
    id: 'register.emailLabel',
    defaultMessage: 'Email',
  },
  displayNameLabel: {
    id: 'register.displayNameLabel',
    defaultMessage: 'Display name',
  },
  passwordLabel: {
    id: 'register.passwordLabel',
    defaultMessage: 'Password',
  },
  registerButton: {
    id: 'register.registerButton',
    defaultMessage: 'Register',
  },
  alreadyHaveAccount: {
    id: 'register.alreadyHaveAccount',
    defaultMessage: 'Already have an account?',
  },
  logInLink: {
    id: 'register.logInLink',
    defaultMessage: 'Log In',
  },
  emailAlreadyExistsError: {
    id: 'register.emailAlreadyExistsError',
    defaultMessage: 'User with this email already exists',
  },
});
