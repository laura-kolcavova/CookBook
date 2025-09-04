import { RecipeDetailIngredientDto } from './RecipeDetailIngredientDto';
import { RecipeDetailInstructionDto } from './RecipeDetailInstructionDto';

export type RecipeDetailDto = {
  id: number;
  userId: number;
  title: string;
  description?: string;
  servings: number;
  preparationTime: number;
  cookTime: number;
  notes?: string;
  tags: string[];
  ingredients: RecipeDetailIngredientDto[];
  instructions: RecipeDetailInstructionDto[];
};
