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

export type RecipeDetailIngredientDto = {
  localId: number;
  note: string;
};

export type RecipeDetailInstructionDto = {
  localId: number;
  note: string;
};
