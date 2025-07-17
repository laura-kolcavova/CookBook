import styled from 'styled-components';
import { Button } from 'reactstrap';

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
    border-radius: 0;
    border-left: 0;
    border-right: 0;
    text-align: center;
    font-weight: 600;
    font-size: 1.1rem;
  }
`;

export const ServingsButton = styled(Button)`
  border-radius: 0.375rem;
  padding: 0.5rem;
  width: 40px;
  height: 38px;
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

export const PresetServingsButton = styled(Button)`
  font-size: 0.75rem;
  padding: 0.25rem 0.5rem;
  min-width: 30px;

  &.active {
    background-color: var(--navbar-background-color);
    border-color: var(--navbar-background-color);
    color: var(--text-primary-color);
  }
`;
