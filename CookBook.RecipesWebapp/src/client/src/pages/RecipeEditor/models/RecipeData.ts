import { IngredientItem } from './IngredientItem';
import { InstructionItem } from './InstructionItem';

export type RecipeData = {
  recipeId?: number;
  title: string;
  description?: string;
  servings: number;
  preparationTime: number;
  cookTime: number;
  notes?: string;
  ingredients: IngredientItem[];
  instructions: InstructionItem[];
  tags: string[];
};
