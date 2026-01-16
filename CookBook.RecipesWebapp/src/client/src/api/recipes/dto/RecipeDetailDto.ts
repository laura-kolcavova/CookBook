export type RecipeDetailDto = {
  id: number;
  userId: number;
  title: string;
  description?: string;
  servings: number;
  cookTime: number;
  notes?: string;
  createdAt: string;
  ingredients: RecipeDetailIngredientDto[];
  instructions: RecipeDetailInstructionDto[];
  tags: string[];
};

export type RecipeDetailIngredientDto = {
  localId: number;
  note: string;
};

export type RecipeDetailInstructionDto = {
  localId: number;
  note: string;
};
