import styled from 'styled-components';

export const TagsContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
`;

export const TagsInputGroup = styled.div`
  display: flex;
  gap: 0.5rem;

  .form-control {
    flex: 1;
  }

  button {
    padding: 0.5rem;
    width: 45px;
    display: flex;
    align-items: center;
    justify-content: center;

    svg {
      font-size: 0.8rem;
    }
  }
`;

export const Tag = styled.span`
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  background: var(--navbar-background-color);
  color: var(--text-primary-color);
  padding: 0.5rem 0.75rem;
  border-radius: 20px;
  font-size: 0.9rem;
  font-weight: 500;

  button {
    background: none;
    border: none;
    color: inherit;
    padding: 0;
    width: auto;
    height: auto;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 50%;
    width: 18px;
    height: 18px;

    &:hover {
      background: rgba(0, 0, 0, 0.1);
    }

    svg {
      font-size: 0.7rem;
    }
  }
`;

export const TagsPresets = styled.div`
  .preset-tag {
    font-size: 0.75rem;
    padding: 0.25rem 0.5rem;

    &:hover {
      background-color: var(--navbar-background-color);
      border-color: var(--navbar-background-color);
      color: var(--text-primary-color);
    }
  }
`;
