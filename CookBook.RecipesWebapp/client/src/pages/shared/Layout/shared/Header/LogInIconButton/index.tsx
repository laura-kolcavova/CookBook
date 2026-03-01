import { FaRegCircleUser } from 'react-icons/fa6';
import { FormattedMessage } from 'react-intl';
import { messages } from '../messages';
import { usersService } from '~/api/users/usersService';
import { pages } from '~/navigation/pages';

export const LogInIconButton = () => {
  return (
    <a
      href={usersService.getLogInUserUrl(pages.Home.paths[0])}
      className="py-1 px-6 flex flex-col justify-center items-center transition-colors duration-150 text-navlink-color hover:text-navlink-color-hover">
      <span className="mb-1">
        <FaRegCircleUser />
      </span>
      <span className="text-center">
        <FormattedMessage {...messages.logIn} />
      </span>
    </a>
  );
};
