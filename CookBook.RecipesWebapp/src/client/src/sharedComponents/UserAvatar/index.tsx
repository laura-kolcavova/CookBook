import React from 'react';
import { IconWrapper, StyledDiv, TextWrapper } from './styled';
import { FaRegCircleUser } from 'react-icons/fa6';

interface IUserAvatarProps {
  title: string;
}

export const UserAvatar: React.FC<IUserAvatarProps> = ({ title }) => {
  return (
    <StyledDiv>
      <IconWrapper>
        <FaRegCircleUser />
      </IconWrapper>
      <TextWrapper>{title}</TextWrapper>
    </StyledDiv>
  );
};
