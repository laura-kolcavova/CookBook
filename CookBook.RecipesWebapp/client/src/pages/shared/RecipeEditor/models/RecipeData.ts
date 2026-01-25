import type { RecipeIngredientData } from './RecipeIngredientData';
import type { RecipeInstructionData } from './RecipeInstructionData';

export type RecipeData = {
  recipeId: number | null;
  title: string;
  description: string | null;
  servings: number;
  cookTime: number;
  notes: string | null;
  ingredients: RecipeIngredientData[];
  instructions: RecipeInstructionData[];
  tags: string[];
};
