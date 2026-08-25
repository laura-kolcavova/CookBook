import { Dialog, DialogPanel, DialogTitle } from '@headlessui/react';
import { useEffect, useState } from 'react';
import { HiXMark as XMarkIcon } from 'react-icons/hi2';
import { FormattedMessage, useIntl } from 'react-intl';
import { validateDisplayName } from '../../utils/displayNameValidator';
import { useChangeDisplayNameErrorMessage } from './hooks/useChangeDisplayNameErrorMessage';
import { messages } from './messages';
import { useModals } from '~/modals/ModalProvider';
import { Alert } from '~/pages/shared/Alert';
import { Button } from '~/pages/shared/Button';
import { FeedbackError } from '~/pages/shared/forms/FeedbackError';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';
import { SpinnerIcon } from '~/pages/shared/icons/SpinnerIcon';
import { useChangeDisplayNameMutation } from './hooks/useChangeDisplayNameMutation';

export type ChangeDisplayNameModalProps = {
  currentDisplayName: string;
};

export const ChangeDisplayNameModal = ({ currentDisplayName }: ChangeDisplayNameModalProps) => {
  const { formatMessage } = useIntl();

  const { hideModal } = useModals();

  const [isOpen, setIsOpen] = useState(false);

  const [displayName, setDisplayName] = useState(currentDisplayName);

  const [validationErrorMessage, setValidationErrorMessage] = useState<string | undefined>();

  const {
    mutate: changeDisplayNameMutate,
    isPending: changeDisplayNameIsPending,
    isError: changeDisplayNameIsError,
    error: changeDisplayNameError,
  } = useChangeDisplayNameMutation();

  const { getErrorMessage } = useChangeDisplayNameErrorMessage();

  useEffect(() => {
    // eslint-disable-next-line react-hooks/set-state-in-effect
    setIsOpen(true);
  }, []);

  const handleSave = () => {
    const validationResult = validateDisplayName(displayName);

    if (!validationResult.isValid) {
      setValidationErrorMessage(validationResult.errorMessage);

      return;
    }

    setValidationErrorMessage(undefined);

    changeDisplayNameMutate(displayName.trim());
  };

  return (
    <Dialog open={isOpen} as="div" className="relative z-50 focus:outline-none" onClose={hideModal}>
      <div className="fixed inset-0 w-screen overflow-y-auto bg-black/25">
        <div className="flex min-h-full items-center justify-center p-4">
          <DialogPanel
            transition
            className="w-full max-w-sm px-6 py-5 rounded-md border-1 bg-modal-background-color border-modal-border-color backdrop-blur-2xl duration-300 ease-out data-closed:transform-[scale(95%)] data-closed:opacity-0">
            <div className="mb-2 h-10 relative">
              <DialogTitle className="text-xl font-medium text-center pr-10 -mr-10 text-text-color-primary">
                <FormattedMessage {...messages.modalTitle} />
              </DialogTitle>

              <div
                className="p-1 cursor-pointer text-gray-600 hover:text-gray-500 absolute -right-2 -top-2"
                onClick={hideModal}>
                <XMarkIcon className="size-6" />
              </div>
            </div>

            <div className="flex-1 py-4">
              {changeDisplayNameIsError && (
                <Alert color="danger" isDismissible={true}>
                  {getErrorMessage(changeDisplayNameError)}
                </Alert>
              )}

              <FormLabel htmlFor="displayName">
                <FormattedMessage {...messages.displayNameLabel} />
              </FormLabel>

              <FormTextInput
                id="displayName"
                type="text"
                placeholder={formatMessage(messages.displayNameLabel)}
                value={displayName}
                onChange={(e) => setDisplayName(e.target.value)}
                autoComplete="off"
                autoFocus
                required
              />

              {validationErrorMessage && <FeedbackError message={validationErrorMessage} />}
            </div>

            <div className="flex items-center justify-center gap-4">
              <Button onClick={hideModal} variant="primary">
                <FormattedMessage {...messages.cancelButton} />
              </Button>

              <Button
                onClick={handleSave}
                className="flex items-center justify-center"
                disabled={changeDisplayNameIsPending}
                variant="primary">
                <span>
                  <FormattedMessage {...messages.saveButton} />
                </span>

                {changeDisplayNameIsPending && <SpinnerIcon className="animate-spin size-4 ml-2" />}
              </Button>
            </div>
          </DialogPanel>
        </div>
      </div>
    </Dialog>
  );
};
