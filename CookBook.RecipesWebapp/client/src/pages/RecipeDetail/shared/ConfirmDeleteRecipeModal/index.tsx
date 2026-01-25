import { Dialog, DialogPanel, DialogTitle } from '@headlessui/react';
import { FormattedMessage } from 'react-intl';
import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { useRemoveErrorMessage } from './hooks/useRemoveRecipeErrorMessage';
import { useRemoveRecipeMutation } from './hooks/useRemoveRecipeMutation';
import { useEffect, useState } from 'react';
import { Alert } from '~/pages/shared/Alert';
import { SpinnerIcon } from '~/pages/shared/icons/SpinnerIcon';
import { Button } from '~/pages/shared/Button';
import { useModals } from '~/modals/ModalProvider';
import { HiXMark as XMarkIcon } from 'react-icons/hi2';
import { messages } from '../../messages';

export type ConfirmDeleteRecipeModalProps = {
  recipe: RecipeDetailDto;
  onDelete: () => void;
};

export const ConfirmDeleteRecipeModal = ({ recipe, onDelete }: ConfirmDeleteRecipeModalProps) => {
  const { hideModal } = useModals();

  const [isOpen, setIsOpen] = useState(false);

  const {
    mutate: removeRecipeMutate,
    isSuccess: removeRecieIsSuccess,
    isPending: removeRecipeIsPending,
    isError: removeRecipeIsError,
    error: removeRecipeError,
  } = useRemoveRecipeMutation(recipe.recipeId);

  const { getErrorMessage } = useRemoveErrorMessage();

  const deleteRecipe = () => {
    removeRecipeMutate();
  };

  useEffect(() => {
    if (removeRecieIsSuccess) {
      hideModal();
      onDelete();
    }
  }, [hideModal, onDelete, removeRecieIsSuccess]);

  useEffect(() => {
    // eslint-disable-next-line react-hooks/set-state-in-effect
    setIsOpen(true);
  }, []);

  return (
    <Dialog open={isOpen} as="div" className="relative z-50 focus:outline-none" onClose={hideModal}>
      <div className="fixed inset-0 w-screen overflow-y-auto bg-black/25">
        <div className="flex min-h-full items-center justify-center p-4">
          <DialogPanel
            transition
            className="px-6 py-5 rounded-md border-1 bg-modal-background-color border-modal-border-color backdrop-blur-2xl duration-300 ease-out data-closed:transform-[scale(95%)] data-closed:opacity-0">
            <div className="mb-2 h-10 relative">
              <DialogTitle className="text-xl font-medium text-center pr-10 -mr-10 text-text-color-primary">
                {recipe.title}
              </DialogTitle>

              <div
                className="p-1 cursor-pointer text-gray-600 hover:text-gray-500 absolute -right-2 -top-2"
                onClick={hideModal}>
                <XMarkIcon className="size-6" />
              </div>
            </div>

            <div className="flex-1 py-8">
              {removeRecipeIsError && (
                <Alert color="danger" isDismissible={true}>
                  {getErrorMessage(removeRecipeError)}
                </Alert>
              )}

              <div className="mb-3 text-center">
                <p className="text-base font-bold text-text-color-primary">
                  <FormattedMessage {...messages.confirmDeleteQuestion} />
                </p>
              </div>

              <div className="mb-3 text-center">
                <p className="text-sm font-semibold text-text-color-secondary">
                  <FormattedMessage {...messages.actionCannotBeUndone} />
                </p>
              </div>
            </div>

            <div className="flex items-center justify-center gap-4">
              <Button
                className="py-2 px-4 rounded font-bold focus:outline-none focus:shadow-outline cursor-pointer flex items-center justify-center bg-pallete-2 hover:bg-pallete-3 text-pallete-8"
                onClick={hideModal}
                variant="primary">
                <FormattedMessage {...messages.cancelButton} />
              </Button>

              <Button
                onClick={deleteRecipe}
                className="flex items-center justify-center"
                disabled={removeRecipeIsPending}
                variant="danger">
                <span>
                  <FormattedMessage {...messages.deleteButton} />
                </span>

                {removeRecipeIsPending && <SpinnerIcon className="animate-spin size-4 ml-2" />}
              </Button>
            </div>
          </DialogPanel>
        </div>
      </div>
    </Dialog>
  );
};
