import { useLogOutUserMutation } from './useLogOutUserMutation';
import { useEffect } from 'react';
import { useLogOutUserErrorMessage } from './useLogOutUserErrorMessage';
import { Slide, toast } from 'react-toastify';

export const useLogOutUser = () => {
  const { mutate, isError, error } = useLogOutUserMutation();

  const { getErrorMessage } = useLogOutUserErrorMessage();

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
