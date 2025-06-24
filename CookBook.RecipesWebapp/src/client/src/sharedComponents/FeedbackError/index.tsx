import React from 'react';
import { StyledDiv } from './styled';

interface IFeedbackErrorProps {
  message: string;
}

export const FeedbackError: React.FC<IFeedbackErrorProps> = ({ message }) => {
  return <StyledDiv>{message}</StyledDiv>;
};
