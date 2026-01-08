import type { IngredientItemDto } from './IngredientItemDto';
import type { InstructionItemDto } from './InstructionItemDto';

export type SaveRecipeRequestDto = {
  recipeId?: number;
  userId: number;
  title: string;
  description?: string;
  servings: number;
  cookTime: number;
  notes?: string;
  ingredients: IngredientItemDto[];
  instructions: InstructionItemDto[];
  tags: string[];
};
