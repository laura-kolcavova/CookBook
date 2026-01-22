import { useLoggedUser } from '~/authentication/LoggedUserProvider';
import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { DeleteRecipeButton } from './shared/DeleteRecipeButton';
import { EditRecipeButton } from './shared/EditRecipeButton';

export type RecipeDetailHeaderProps = {
  recipe: RecipeDetailDto | null | undefined;
};

export const RecipeDetailHeader = ({ recipe }: RecipeDetailHeaderProps) => {
  const { isAuthenticated, user } = useLoggedUser();

  return (
    <div className="flex items-center justify-end gap-4 mb-4">
      {recipe && isAuthenticated && recipe.userId === user.userId && (
        <>
          <EditRecipeButton recipe={recipe} />

          <DeleteRecipeButton recipe={recipe} />
        </>
      )}
    </div>
  );
};
