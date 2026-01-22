import { RecipeEditor } from '../shared/RecipeEditor';

export const AddRecipe = () => {
  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        <h2 className="text-2xl font-semibold text-text-color-primary mb-6">Add Recipe</h2>
        <RecipeEditor />
      </div>
    </div>
  );
};
