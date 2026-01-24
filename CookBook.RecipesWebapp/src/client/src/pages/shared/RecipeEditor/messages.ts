import { defineMessages } from 'react-intl';

export const messages = defineMessages({
  saveButton: {
    id: 'shared.recipeEditor.saveButton',
    defaultMessage: 'Save',
  },
  recipeTitleLabel: {
    id: 'shared.recipeEditor.recipeTitleLabel',
    defaultMessage: 'Recipe Title *',
  },
  recipeTitlePlaceholder: {
    id: 'shared.recipeEditor.recipeTitlePlaceholder',
    defaultMessage: 'Give your recipe a name',
  },
  descriptionLabel: {
    id: 'shared.recipeEditor.descriptionLabel',
    defaultMessage: 'Description',
  },
  descriptionPlaceholder: {
    id: 'shared.recipeEditor.descriptionPlaceholder',
    defaultMessage: 'Introduce your recipe',
  },
  servingsLabel: {
    id: 'shared.recipeEditor.servingsLabel',
    defaultMessage: 'Number of Servings',
  },
  portionsCount: {
    id: 'shared.recipeEditor.portionsCount',
    defaultMessage: '{count, plural, one {1 portion} other {# portions}}',
  },
  cookTimeLabel: {
    id: 'shared.recipeEditor.cookTimeLabel',
    defaultMessage: 'Cook Time',
  },
  ingredientsLabel: {
    id: 'shared.recipeEditor.ingredientsLabel',
    defaultMessage: 'Ingredients',
  },
  ingredientPlaceholder: {
    id: 'shared.recipeEditor.ingredientPlaceholder',
    defaultMessage: 'e.g., 2 cups flour, 1 tsp salt...',
  },
  addIngredientButton: {
    id: 'shared.recipeEditor.addIngredientButton',
    defaultMessage: 'Add Ingredient',
  },
  ingredientsHelpText: {
    id: 'shared.recipeEditor.ingredientsHelpText',
    defaultMessage: 'Include amounts and units (e.g., "2 cups flour", "1 tsp vanilla extract")',
  },
  instructionsLabel: {
    id: 'shared.recipeEditor.instructionsLabel',
    defaultMessage: 'Instructions',
  },
  instructionPlaceholder: {
    id: 'shared.recipeEditor.instructionPlaceholder',
    defaultMessage: 'Describe this step in detail...',
  },
  addStepButton: {
    id: 'shared.recipeEditor.addStepButton',
    defaultMessage: 'Add step',
  },
  instructionsHelpText: {
    id: 'shared.recipeEditor.instructionsHelpText',
    defaultMessage: 'Be specific with temperatures, times, and techniques for best results',
  },
  notesLabel: {
    id: 'shared.recipeEditor.notesLabel',
    defaultMessage: 'Notes',
  },
  notesPlaceholder: {
    id: 'shared.recipeEditor.notesPlaceholder',
    defaultMessage: 'Any special tips or notes for this recipe...',
  },
  tagsLabel: {
    id: 'shared.recipeEditor.tagsLabel',
    defaultMessage: 'Tags',
  },
});
