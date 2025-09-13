import styled from 'styled-components';
import { Button } from '~/sharedComponents/Button';

export const TimeInputContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
`;

export const TimeInputGroupContainer = styled.div`
  display: flex;
  flex-direction: row;
  gap: 1rem;
`;

export const TimeInputGroup = styled.div`
  display: flex;
  align-items: center;
  max-width: 190px;

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

export const TimeInputGroupText = styled.div`
  height: 2.5rem;
  background-color: #f8f9fa;
  border-color: #dee2e6;
  color: #6c757d;
  font-size: 0.875rem;
  justify-content: center;
  border-radius: 0;
`;

export const TimeButton = styled(Button)`
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
