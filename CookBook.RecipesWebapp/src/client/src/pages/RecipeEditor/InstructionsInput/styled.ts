import styled from 'styled-components';
import { Button } from '~/sharedComponents/Button';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';

export const InstructionsContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1rem;
`;

export const InstructionItem = styled.div`
  .input-group {
    .input-group-text {
      background-color: #f8f9fa;
      border-color: #dee2e6;
      color: #6c757d;
      font-weight: 600;
      min-width: 70px;
      justify-content: center;
      align-items: flex-start;
      padding-top: 0.75rem;
    }

    .form-control {
      resize: vertical;
      min-height: 60px;
    }
  }
`;

export const AddButton = styled(Button)`
  align-self: flex-start;
  font-size: 0.9rem;

  svg {
    font-size: 0.8rem;
  }
`;

export const StyledInputGroupText = styled.div`
  align-items: center !important;
`;

export const StyledTextArea = styled(FormTextInput)`
  resize: none;
`;

export const RemoveButton = styled(Button)`
  border-top-left-radius: 0;
  border-bottom-left-radius: 0;
  padding: 0.5rem;
  width: 45px;
  display: flex;
  align-items: center;
  justify-content: center;
  padding-top: 0.75rem;

  svg {
    font-size: 0.8rem;
  }

  &:disabled {
    opacity: 0.5;
  }
`;
