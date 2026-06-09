export type GetLatestRecipesResponseDto = {
  latestRecipes: LatestRecipeDto[];
};

export type LatestRecipeDto = {
  recipeId: number;
  title: string;
  description: string | null;
  createdAt: string;
  imageUrl: string;
};
