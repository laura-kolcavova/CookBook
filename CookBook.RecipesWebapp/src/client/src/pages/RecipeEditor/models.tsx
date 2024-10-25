export type RecipeData = {
  title: string;
  description: string | null;
  servings: number;
  preparationTime: number;
  cookTime: number;
  notes: string | null;
  tags: string[];
};

export const EMPTY_RECIPE_DATA: RecipeData = {
  title: '',
  description: null,
  servings: 0,
  preparationTime: 0,
  cookTime: 0,
  notes: null,
  tags: [],
};
