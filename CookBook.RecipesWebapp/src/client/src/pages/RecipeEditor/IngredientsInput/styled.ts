import styled from 'styled-components';
import { Button } from '~/sharedComponents/Button';

export const IngredientsContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
`;

export const IngredientItem = styled.div`
  .input-group {
    .input-group-text {
      background-color: #f8f9fa;
      border-color: #dee2e6;
      color: #6c757d;
      font-weight: 600;
      min-width: 40px;
      justify-content: center;
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

export const RemoveButton = styled(Button)`
  border-top-left-radius: 0;
  border-bottom-left-radius: 0;
  padding: 0.5rem;
  width: 45px;
  display: flex;
  align-items: center;
  justify-content: center;

  svg {
    font-size: 0.8rem;
  }

  &:disabled {
    opacity: 0.5;
  }
`;
