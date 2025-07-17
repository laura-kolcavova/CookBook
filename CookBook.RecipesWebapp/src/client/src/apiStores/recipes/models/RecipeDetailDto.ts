export type RecipeDetailDto = {
  id: number;
  userId: number;
  title: string;
  description?: string;
  servings: number;
  preparationTime: number;
  cookTime: number;
  notes?: string;
  tags: string[];
  ingredients: IngredientItemDetailDto[];
  instructions: InstructionItemDetailDto[];
};

export type IngredientItemDetailDto = {
  localId: number;
  note: string;
};

export type InstructionItemDetailDto = {
  localId: number;
  note: string;
};
