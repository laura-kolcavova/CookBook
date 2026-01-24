import { FormattedMessage } from 'react-intl';
import { messages } from './messages';

export const NotFound = () => {
  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        <div className="py-24 text-center">
          <span className="text-xl font-bold text-text-color-primary">
            <FormattedMessage {...messages.pageNotFound} />
          </span>
        </div>
      </div>
    </div>
  );
};
