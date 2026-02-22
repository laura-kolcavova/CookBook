import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import { useLogOutUserMutation } from './useLogOutUserMutation';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { pages } from '~/navigation/pages';
import { useLogOutUserErrorMessage } from './useLogOutUserErrorMessage';
import { Slide, toast } from 'react-toastify';

export const useLogOutUser = () => {
  const navigate = useNavigate();

  const { resetCurrentUser } = useCurrentUser();

  const { mutate, isSuccess, isError, error } = useLogOutUserMutation();

  const { getErrorMessage } = useLogOutUserErrorMessage();

  useEffect(() => {
    if (isSuccess) {
      resetCurrentUser();
      navigate(pages.Home.paths[0]);
    }
  }, [isSuccess, navigate, resetCurrentUser]);

  useEffect(() => {
    if (isError) {
      const errorMessage = getErrorMessage(error);

      toast.error(errorMessage, {
        position: 'bottom-right',
        hideProgressBar: true,
        autoClose: 5000,
        transition: Slide,
      });
    }
  }, [error, getErrorMessage, isError]);

  return {
    logOutUser: mutate,
  };
};
