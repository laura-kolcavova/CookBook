export type RecipeDetailDto = {
  recipeId: number;
  title: string;
  description: string | null;
  servings: number;
  cookTime: number;
  notes: string | null;
  createdAt: string;
  userProfileInfo: UserProfileInfoDto;
  ingredients: RecipeDetailIngredientDto[];
  instructions: RecipeDetailInstructionDto[];
  tags: string[];
};

export type UserProfileInfoDto = {
  userName: string;
  displayName: string;
};

export type RecipeDetailIngredientDto = {
  localId: number;
  note: string;
};

export type RecipeDetailInstructionDto = {
  localId: number;
  note: string;
};
