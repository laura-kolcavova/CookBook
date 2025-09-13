import styled from 'styled-components';
import { Button } from '~/sharedComponents/Button';

export const ServingsContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
`;

export const ServingsInputGroup = styled.div`
  display: flex;
  align-items: center;
  max-width: 150px;

  .form-control {
    height: 2.5rem;
    text-align: center;
    font-weight: 600;
    font-size: 1.1rem;
    border-radius: 0;
    border-left: 0;
    border-right: 0;
  }
`;

export const ServingsButton = styled(Button)`
  border-radius: 0.375rem;
  padding: 0.5rem;
  width: 2.5rem;
  height: 2.5rem;
  display: flex;
  align-items: center;
  justify-content: center;

  &:first-child {
    border-top-right-radius: 0;
    border-bottom-right-radius: 0;
  }

  &:last-child {
    border-top-left-radius: 0;
    border-bottom-left-radius: 0;
  }

  svg {
    font-size: 0.75rem;
  }

  &:disabled {
    opacity: 0.5;
  }
`;
