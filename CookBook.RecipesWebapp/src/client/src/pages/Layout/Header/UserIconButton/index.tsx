import React, { useContext, useState } from 'react';

import { Pages } from '../../../../navigation/pages';

import { UserIdentityContext } from '~/contexts/UserIdentityContext';
import { useAtomValue } from 'jotai';
import { userAtom } from '~/atoms/userAtom';
import { FaRegCircleUser } from 'react-icons/fa6';
import {
  Dropdown,
  DropdownButton,
  DropDownItem,
  DropdownMenu,
  IconWrapper,
  TextWrapper,
} from './styled';
import { useRouter } from '~/navigation/hooks/useRouter';

export const UserIconButton: React.FC = () => {
  const { logout } = useContext(UserIdentityContext);

  const { goToPage } = useRouter();

  const { nameClaimType } = useAtomValue(userAtom);

  const [isOpen, setIsOpen] = useState<boolean>(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <Dropdown>
      <DropdownButton onClick={toggle} isOpen={isOpen}>
        <IconWrapper>
          <FaRegCircleUser />
        </IconWrapper>
        <TextWrapper>{nameClaimType}</TextWrapper>
      </DropdownButton>

      <DropdownMenu isOpen={isOpen}>
        <DropDownItem
          onClick={() => {
            goToPage(Pages.MyProfile);
          }}>
          My profile
        </DropDownItem>

        <DropDownItem onClick={logout}>Log out</DropDownItem>
      </DropdownMenu>
    </Dropdown>
  );
};
