import { createGlobalStyle } from 'styled-components';

const GlobalStyle = createGlobalStyle`
  .toast__container {
    position: absolute;
  }

  #content {
    padding: 0;
  }

  .content-wrapper {
      margin-left: 7.688rem;
  }

  .modal-content {
    height: 100%;
  }

  .page-header {
    margin-top: 0;
    height: 4rem;
  }

  .loading--small
  .loading__icon {
    margin-bottom: 0;
  }
`;

export default GlobalStyle;
