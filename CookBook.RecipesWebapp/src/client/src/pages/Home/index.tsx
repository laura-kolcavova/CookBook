import React, { useContext } from 'react';
import { Button } from 'reactstrap';
import { UserIdentityContext } from '~/contexts/UserIdentityContext';
import { useRouter } from '~/navigation/hooks/useRouter';
import { Pages } from '~/navigation/pages';
import { ActionButtons } from './styled';

export const Home: React.FC = () => {
  const { user } = useContext(UserIdentityContext);
  const { goToPage } = useRouter();

  const handleCreateNewRecipeButtonClick = () => {
    goToPage(Pages.AddRecipe);
  };
  return (
    <>
      {user.isAuthenticated && (
        <ActionButtons>
          <Button color="primary" onClick={handleCreateNewRecipeButtonClick}>
            Create new recipe
          </Button>
        </ActionButtons>
      )}
    </>
  );
};
