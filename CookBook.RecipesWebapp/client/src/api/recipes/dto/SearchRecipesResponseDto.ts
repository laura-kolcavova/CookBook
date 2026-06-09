export type SearchRecipesResponseDto = {
  recipes: RecipeSearchItemDto[];
};

export type RecipeSearchItemDto = {
  recipeId: string;
  title: string;
  createdAt: string;
  imageUrl: string;
};
