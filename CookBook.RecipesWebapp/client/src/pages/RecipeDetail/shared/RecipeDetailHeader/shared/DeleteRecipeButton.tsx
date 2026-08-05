import { FormattedMessage } from 'react-intl';
import { Button } from '~/pages/shared/Button';
import { useModals } from '~/modals/ModalProvider';
import { ConfirmDeleteRecipeModal } from '../../ConfirmDeleteRecipeModal';
import { messages } from '~/pages/RecipeDetail/messages';
import type { RecipeDetailDto } from '~/api/recipes/dto/GetRecipeDetailResponseDto';

export type DeleteRecipeButtonProps = {
  recipe: RecipeDetailDto;
};

export const DeleteRecipeButton = ({ recipe }: DeleteRecipeButtonProps) => {
  const { openModal } = useModals();

  const openConfirmDeleteModal = () => {
    openModal(<ConfirmDeleteRecipeModal recipe={recipe} />);
  };

  return (
    <Button onClick={openConfirmDeleteModal} variant="danger">
      <FormattedMessage {...messages.deleteButton} />
    </Button>
  );
};
