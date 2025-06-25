import React from 'react';
import { Button } from 'reactstrap';

import { useRouter } from '~/navigation/hooks/useRouter';
import { Pages } from '~/navigation/pages';
import { ActionButtons } from './styled';

import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';

export const Home: React.FC = () => {
  const { isAuthenticated } = useAtomValue(userAtom);

  const { goToPage } = useRouter();

  const handleCreateNewRecipeButtonClick = () => {
    goToPage(Pages.AddRecipe);
  };
  return (
    <>
      {isAuthenticated && (
        <ActionButtons>
          <Button color="primary" onClick={handleCreateNewRecipeButtonClick}>
            Create new recipe
          </Button>
        </ActionButtons>
      )}
    </>
  );
};
