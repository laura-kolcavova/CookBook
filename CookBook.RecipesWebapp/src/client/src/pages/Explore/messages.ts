import { defineMessages } from 'react-intl';

export const messages = defineMessages({
  exploreRecipesTitle: {
    id: 'explore.exploreRecipesTitle',
    defaultMessage: 'Explore Recipes',
  },
  searchPlaceholder: {
    id: 'explore.searchPlaceholder',
    defaultMessage: 'Search for recipes by title, description, or tags...',
  },
  searchButton: {
    id: 'explore.searchButton',
    defaultMessage: 'Search',
  },
  searchError: {
    id: 'explore.searchError',
    defaultMessage: 'Something went wrong while searching for recipes',
  },
  searching: {
    id: 'explore.searching',
    defaultMessage: 'Searching...',
  },
  noRecipesFound: {
    id: 'explore.noRecipesFound',
    defaultMessage: 'No recipes found matching "{searchTerm}"',
  },
  noRecipesCreatedYet: {
    id: 'explore.noRecipesCreatedYet',
    defaultMessage: 'No recipes were created yet',
  },
  showMoreButton: {
    id: 'explore.showMoreButton',
    defaultMessage: 'Show More',
  },
});
