import React, { useContext, useState } from 'react';

import { Dropdown, DropdownItem, DropdownMenu } from 'reactstrap';
import { Pages } from '../../../../navigation/pages';
import { DropdownWrapper, StyledDropdownToggle } from './styled';
import { UserAvatar } from 'src/sharedComponents/UserAvatar';
import { UserIdentityContext } from 'src/contexts/UserIdentityContext';
import { useRouter } from 'src/navigation/hooks/useRouter';

export const UserMenuItem: React.FC = () => {
  const { user, logout } = useContext(UserIdentityContext);
  const { goToPage } = useRouter();

  const [isOpen, setIsOpen] = useState<boolean>(false);

  const toggle = () => setIsOpen(!isOpen);

  const handleLogOutButtonClick = () => {
    logout();
  };

  return (
    <DropdownWrapper>
      <Dropdown isOpen={isOpen} toggle={toggle} direction="down" inNavbar={true} nav={true}>
        <StyledDropdownToggle tag="span">
          <UserAvatar title={user.nameClaimType} />
        </StyledDropdownToggle>

        <DropdownMenu>
          <DropdownItem
            onClick={() => {
              goToPage(Pages.MyProfile);
            }}>
            My profile
          </DropdownItem>
          <DropdownItem onClick={handleLogOutButtonClick}>Log out</DropdownItem>
        </DropdownMenu>
      </Dropdown>
    </DropdownWrapper>
  );
};
