export type SaveRecipeRequestDto = {
  recipeId: number | null;
  userName: string;
  title: string;
  description: string | null;
  servings: number;
  cookTime: number;
  notes: string | null;
  ingredients: IngredientItemDto[];
  instructions: InstructionItemDto[];
  tags: string[];
};

export type IngredientItemDto = {
  localId: number | null;
  note: string;
};

export type InstructionItemDto = {
  localId: number | null;
  note: string;
};
