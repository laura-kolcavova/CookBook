import { createGlobalStyle } from 'styled-components';

const GlobalStyle = createGlobalStyle`
  :root {
    --text-primary-color: #323d47;

    --navbar-background-color: #fce3d6;

    --navbrand-color: #323d47;

    --navlink-color: rgba(117, 117, 117, 0.8);
    --navlink-color-hover: #555;
    --navlink-color-active: #555;
  }

  body {
    margin: 0;
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu',
      'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
  }
`;

export default GlobalStyle;
