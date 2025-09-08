import styled from 'styled-components';

type DropdownButtonProps = {
  isOpen: boolean;
};

export const DropdownButton = styled.button<DropdownButtonProps>`
  color: ${(props) => (props.isOpen ? 'var(--navlink-color-active)' : 'var(--navlink-color)')};
  padding: 0.2rem 1.5rem;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  cursor: pointer;

  &:hover {
    color: var(--navlink-color-hover);
  }
`;

export const IconWrapper = styled.span`
  margin-bottom: 0.2rem;
`;

export const TextWrapper = styled.span`
  text-align: center;
`;

export const Dropdown = styled.div`
  position: relative;
`;

type DropdownMenuProps = {
  isOpen: boolean;
};

export const DropdownMenu = styled.div<DropdownMenuProps>`
  position: absolute;
  z-index: 1;
  bottom: -0.5rem;
  right: 0;
  transform: translateY(100%);
  background-color: #fff;
  min-width: 10rem;
  padding: 0.5rem 0;
  border: 1px solid black;
  border-radius: 0.375rem;
  visibility: ${(props) => (props.isOpen ? 'visible' : 'hidden')};
`;

export const DropDownItem = styled.button`
  width: 100%;
  text-align: left;
  padding: 0.5rem 1rem;
  line-height: 1.5rem;
  cursor: pointer;

  color: var(--navlink-color);

  &:hover {
    color: var(--navlink-color-hover);
  }
`;
