import { FaRegCircleUser } from 'react-icons/fa6';
import { FormattedMessage } from 'react-intl';
import { messages } from '../messages';
import { usersService } from '~/api/users/usersService';
import { pages } from '~/navigation/pages';

export const LogInIconButton = () => {
  const handleClick = () => {
    usersService.redirectTologInUser(pages.Home.paths[0]);
  };

  return (
    <button
      onClick={handleClick}
      className="py-1 px-6 flex flex-col justify-center items-center transition-colors duration-150 text-navlink-color hover:text-navlink-color-hover cursor-pointer">
      <span className="mb-1">
        <FaRegCircleUser />
      </span>
      <span className="text-center">
        <FormattedMessage {...messages.logIn} />
      </span>
    </button>
  );
};
