import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { FormattedMessage } from 'react-intl';
import { Button } from '~/pages/shared/Button';
import { useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { pages } from '~/navigation/pages';
import { useModals } from '~/modals/ModalProvider';
import { ConfirmDeleteRecipeModal } from '../../ConfirmDeleteRecipeModal';
import { messages } from '~/pages/RecipeDetail/messages';

export type DeleteRecipeButtonProps = {
  recipe: RecipeDetailDto;
};

export const DeleteRecipeButton = ({ recipe }: DeleteRecipeButtonProps) => {
  const { openModal } = useModals();

  const navigate = useNavigate();

  const redirectToHome = useCallback(() => {
    navigate(pages.Home.paths[0]);
  }, [navigate]);

  const openConfirmDeleteModal = () => {
    openModal(<ConfirmDeleteRecipeModal recipe={recipe} onDelete={redirectToHome} />);
  };

  return (
    <Button onClick={openConfirmDeleteModal} variant="danger">
      <FormattedMessage {...messages.deleteButton} />
    </Button>
  );
};
