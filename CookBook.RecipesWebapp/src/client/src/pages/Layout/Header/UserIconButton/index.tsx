import React, { useContext, useState } from 'react';
import { Pages } from '../../../../navigation/pages';
import { UserIdentityContext } from '~/contexts/UserIdentityContext';
import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';
import { FaRegCircleUser } from 'react-icons/fa6';
import { useRouter } from '~/navigation/hooks/useRouter';

export const UserIconButton: React.FC = () => {
  const { logout } = useContext(UserIdentityContext);
  const { goToPage } = useRouter();
  const { nameClaimType } = useAtomValue(userAtom);
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const toggle = () => setIsOpen(!isOpen);

  return (
    <div className="relative">
      <button
        onClick={toggle}
        className={`py-1 px-6 flex flex-col justify-center items-center cursor-pointer transition-colors duration-150
          ${isOpen ? 'text-orange-600 font-bold' : 'text-gray-700'} hover:text-orange-500`}
        type="button">
        <span className="mb-1">
          <FaRegCircleUser />
        </span>
        <span className="text-center">{nameClaimType}</span>
      </button>

      <div
        className={`absolute z-10 bottom-[-0.5rem] right-0 translate-y-full bg-white min-w-[10rem] p-2 border border-black rounded-md shadow-lg transition-all duration-150
          ${isOpen ? 'visible' : 'invisible'}`}>
        <button
          className="w-full text-left py-2 px-4 leading-6 cursor-pointer text-gray-700 hover:text-orange-500"
          onClick={() => {
            goToPage(Pages.MyProfile);
            setIsOpen(false);
          }}>
          My profile
        </button>

        <button
          className="w-full text-left py-2 px-4 leading-6 cursor-pointer text-gray-700 hover:text-orange-500"
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
