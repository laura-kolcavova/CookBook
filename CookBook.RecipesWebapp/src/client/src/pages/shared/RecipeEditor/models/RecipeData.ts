import type { IngredientItemData } from './IngredientItemData';
import type { InstructionItemData } from './InstructionItemData';

export type RecipeData = {
  recipeId?: number;
  title: string;
  description?: string;
  servings: number;
  cookTime: number;
  notes?: string;
  ingredients: IngredientItemData[];
  instructions: InstructionItemData[];
  tags: string[];
};
