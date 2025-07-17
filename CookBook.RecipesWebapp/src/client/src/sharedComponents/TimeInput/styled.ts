import styled from 'styled-components';
import { Button } from 'reactstrap';

export const TimeInputContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
`;

export const TimeUnitSelector = styled.div`
  .input-group {
    .form-control {
      text-align: center;
      &:first-child {
        border-top-right-radius: 0;
        border-bottom-right-radius: 0;
      }
      &:last-child {
        border-top-left-radius: 0;
        border-bottom-left-radius: 0;
      }
    }

    .input-group-text {
      background-color: #f8f9fa;
      border-color: #dee2e6;
      color: #6c757d;
      font-size: 0.875rem;
      min-width: 60px;
      justify-content: center;
    }
  }
`;

export const PresetButton = styled(Button)`
  font-size: 0.75rem;
  padding: 0.25rem 0.5rem;

  &.active {
    background-color: var(--navbar-background-color);
    border-color: var(--navbar-background-color);
    color: var(--text-primary-color);
  }
`;
