import { IngredientItemDto } from './IngredientItemDto';
import { InstructionItemDto } from './InstructionItemDto';

export type SaveRecipeRequestDto = {
  recipeId?: number;
  userId: number;
  title: string;
  descripiton?: string;
  servings: number;
  preparationTime: number;
  cookTime: number;
  notes?: string;
  ingredients: IngredientItemDto[];
  instructions: InstructionItemDto[];
  tags: string[];
};
