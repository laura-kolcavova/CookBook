import { FormattedMessage } from 'react-intl';
import { ConfirmDeleteRecipeModal } from '../../ConfirmDeleteRecipeModal';
import type { RecipeDetailDto } from '~/api/recipes/dto/GetRecipeDetailResponseDto';
import { useModals } from '~/modals/ModalProvider';
import { messages } from '~/pages/RecipeDetail/messages';
import { Button } from '~/pages/shared/Button';

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
