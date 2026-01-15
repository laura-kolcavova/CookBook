export type SaveRecipeRequestDto = {
  recipeId?: number;
  userId: number;
  title: string;
  description?: string;
  servings: number;
  cookTime: number;
  notes?: string;
  ingredients: IngredientItemDto[];
  instructions: InstructionItemDto[];
  tags: string[];
};

export type IngredientItemDto = {
  localId?: number;
  note: string;
};

export type InstructionItemDto = {
  localId?: number;
  note: string;
};
