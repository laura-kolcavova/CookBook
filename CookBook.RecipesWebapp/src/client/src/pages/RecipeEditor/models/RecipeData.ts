import { IngredientItemData } from './IngredientItemData';
import { InstructionItemData } from './InstructionItemData';

export type RecipeData = {
  recipeId?: number;
  title: string;
  description?: string;
  servings: number;
  preparationTime: number;
  cookTime: number;
  notes?: string;
  ingredients: IngredientItemData[];
  instructions: InstructionItemData[];
  tags: string[];
};
