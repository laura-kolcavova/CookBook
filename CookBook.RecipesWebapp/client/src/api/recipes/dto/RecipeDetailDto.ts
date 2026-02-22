export type RecipeDetailDto = {
  recipeId: number;
  userName: string;
  title: string;
  description: string | null;
  servings: number;
  cookTime: number;
  notes: string | null;
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
