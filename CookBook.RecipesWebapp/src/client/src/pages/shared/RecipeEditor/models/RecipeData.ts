import type { RecipeIngredientData } from './RecipeIngredientData';
import type { RecipeInstructionData } from './RecipeInstructionData';

export type RecipeData = {
  recipeId?: number;
  title: string;
  description?: string;
  servings: number;
  cookTime: number;
  notes?: string;
  ingredients: RecipeIngredientData[];
  instructions: RecipeInstructionData[];
  tags: string[];
};
