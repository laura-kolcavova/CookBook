import { FaRegCircleUser } from 'react-icons/fa6';
import { FormattedMessage } from 'react-intl';
import { messages } from './messages';
import { EditDisplayNameModal } from './shared/EditDisplayNameModal';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import { useModals } from '~/modals/ModalProvider';
import { Button } from '~/pages/shared/Button';
import { FormLabel } from '~/pages/shared/forms/FormLabel';

export const MyProfile = () => {
  const { currentUser } = useCurrentUser();

  const { openModal } = useModals();

  const handleChangeDisplayName = () => {
    openModal(<EditDisplayNameModal currentDisplayName={currentUser.displayName} />);
  };

  return (
    <div className="bg-content-background-color-primary">
      <div className="page-container mx-auto py-10 px-4">
        <div className="max-w-2xl mx-auto">
          <h1 className="text-2xl font-semibold text-text-color-primary mb-8 text-center">
            <FormattedMessage {...messages.myProfileTitle} />
          </h1>

          <div className="flex flex-col items-center mb-8">
            <FaRegCircleUser className="size-20 text-navlink-color" />
          </div>

          <div className="flex flex-col gap-4">
            <div>
              <FormLabel>
                <FormattedMessage {...messages.displayNameLabel} />
              </FormLabel>
              <div className="flex items-center justify-between gap-4">
                <p className="text-base text-text-color-primary">{currentUser.displayName}</p>

                <Button onClick={handleChangeDisplayName} variant="primary">
                  <FormattedMessage {...messages.changeDisplayNameButton} />
                </Button>
              </div>
            </div>

            <div>
              <FormLabel>
                <FormattedMessage {...messages.userNameLabel} />
              </FormLabel>
              <p className="text-base text-text-color-primary">{currentUser.userName}</p>
            </div>

            <div>
              <FormLabel>
                <FormattedMessage {...messages.emailLabel} />
              </FormLabel>
              <p className="text-base text-text-color-primary">{currentUser.email}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
