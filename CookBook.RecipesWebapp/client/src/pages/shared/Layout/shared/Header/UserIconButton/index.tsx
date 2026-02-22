import { useState, useRef, useEffect } from 'react';
import { FormattedMessage } from 'react-intl';
import { FaRegCircleUser } from 'react-icons/fa6';
import { useNavigate } from 'react-router-dom';
import { pages } from '~/navigation/pages';
import { messages } from '../messages';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';

import { useLogOutUser } from './hooks/useLogOutUser';

export const UserIconButton = () => {
  const navigate = useNavigate();

  const { currentUser } = useCurrentUser();

  const { logOutUser } = useLogOutUser();

  const [isDropdownOpen, setIsDropdownOpen] = useState<boolean>(false);

  const dropdownRef = useRef<HTMLDivElement>(null);

  const toggleDropdown = () => setIsDropdownOpen(!isDropdownOpen);

  const closeDropdown = () => setIsDropdownOpen(false);

  useEffect(() => {
    if (!isDropdownOpen) {
      return;
    }

    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        closeDropdown();
      }
    };
    document.addEventListener('mousedown', handleClickOutside);

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [isDropdownOpen]);

  const isOpenDropdownButtonStyles = isDropdownOpen
    ? 'text-navlink-color-active'
    : 'text-navlink-color';

  const isOpenDropdownListStyled = isDropdownOpen ? 'visible' : 'invisible';

  return (
    <div className="relative" ref={dropdownRef}>
      <button
        onClick={toggleDropdown}
        className={`py-1 px-6 flex flex-col justify-center items-center cursor-pointer transition-colors duration-150
          ${isOpenDropdownButtonStyles} hover:text-navlink-color-hover`}
        type="button">
        <span className="mb-1">
          <FaRegCircleUser />
        </span>
        <span className="text-center">{currentUser.displayName}</span>
      </button>

      <div
        className={`absolute z-10 bottom-[-0.5rem] right-0 translate-y-full bg-white min-w-[10rem] p-2 border border-black rounded-md shadow-lg transition-all duration-150
          ${isOpenDropdownListStyled}`}>
        <button
          className="w-full text-left py-2 px-4 leading-6 cursor-pointer text-navlink-color hover:text-navlink-color-hover"
          onClick={() => {
            navigate(pages.MyProfile.paths[0]);
            closeDropdown();
          }}>
          <FormattedMessage {...messages.myProfile} />
        </button>

        <button
          className="w-full text-left py-2 px-4 leading-6 cursor-pointer text-navlink-color hover:text-navlink-color-hover"
          onClick={() => {
            logOutUser();
            closeDropdown();
          }}>
          <FormattedMessage {...messages.logOut} />
        </button>
      </div>
    </div>
  );
};
