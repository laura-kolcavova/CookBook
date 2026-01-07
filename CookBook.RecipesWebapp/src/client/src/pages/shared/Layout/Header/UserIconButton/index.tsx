import React, { useContext, useState, useRef, useEffect } from 'react';
import { Pages } from '../../../../navigation/pages';
import { UserIdentityContext } from '~/authentication/UserIdentityProvider';
import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';
import { FaRegCircleUser } from 'react-icons/fa6';
import { useRouter } from '~/navigation/hooks/useRouter';

export const UserIconButton: React.FC = () => {
  const { logout } = useContext(UserIdentityContext);
  const { goToPage } = useRouter();
  const { nameClaimType } = useAtomValue(userAtom);
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const dropdownRef = useRef<HTMLDivElement>(null);

  const toggle = () => setIsOpen(!isOpen);

  useEffect(() => {
    if (!isOpen) {
      return;
    }

    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [isOpen]);

  const isOpenDropdownButtonStyles = isOpen ? 'navlink-color-active' : 'navlink-color';
  const isOpenDropdownListStyled = isOpen ? 'visible' : 'invisible';

  return (
    <div className="relative" ref={dropdownRef}>
      <button
        onClick={toggle}
        className={`py-1 px-6 flex flex-col justify-center items-center cursor-pointer transition-colors duration-150
          ${isOpenDropdownButtonStyles} navlink-color-hover`}
        type="button">
        <span className="mb-1">
          <FaRegCircleUser />
        </span>
        <span className="text-center">{nameClaimType}</span>
      </button>

      <div
        className={`absolute z-10 bottom-[-0.5rem] right-0 translate-y-full bg-white min-w-[10rem] p-2 border border-black rounded-md shadow-lg transition-all duration-150
          ${isOpenDropdownListStyled}`}>
        <button
          className="w-full text-left py-2 px-4 leading-6 cursor-pointer navlink-color navlink-color-hover"
          onClick={() => {
            goToPage(Pages.MyProfile);
            setIsOpen(false);
          }}>
          My profile
        </button>

        <button
          className="w-full text-left py-2 px-4 leading-6 cursor-pointer navlink-color navlink-color-hover"
          onClick={() => {
            logout();
            setIsOpen(false);
          }}>
          Log out
        </button>
      </div>
    </div>
  );
};
