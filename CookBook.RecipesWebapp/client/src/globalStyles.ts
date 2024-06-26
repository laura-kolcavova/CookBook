import { createGlobalStyle } from 'styled-components';

const GlobalStyle = createGlobalStyle`
  .toast__container {
    position: absolute;
  }

  .modal-content {
    height: 100%;
  }

  .loading--small
  .loading__icon {
    margin-bottom: 0;
  }
`;

export default GlobalStyle;

