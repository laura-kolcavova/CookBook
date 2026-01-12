import type { RecipeDetailIngredientDto } from './RecipeDetailIngredientDto';
import type { RecipeDetailInstructionDto } from './RecipeDetailInstructionDto';

export type RecipeDetailDto = {
  id: number;
  userId: number;
  title: string;
  description?: string;
  servings: number;
  cookTime: number;
  notes?: string;
  tags: string[];
  ingredients: RecipeDetailIngredientDto[];
  instructions: RecipeDetailInstructionDto[];
};
