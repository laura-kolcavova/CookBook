import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { DeleteRecipeButton } from './shared/DeleteRecipeButton';
import { EditRecipeButton } from './shared/EditRecipeButton';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';

export type RecipeDetailHeaderProps = {
  recipe: RecipeDetailDto | null | undefined;
};

export const RecipeDetailHeader = ({ recipe }: RecipeDetailHeaderProps) => {
  const { currentUser } = useCurrentUser();

  return (
    <div className="flex items-center justify-end gap-4 mb-4">
      {/* // {recipe && currentUser.isAuthenticated && recipe.userId === currentUser.userNumer && ( */}
      {recipe && currentUser.isAuthenticated && recipe.userId === 1 && (
        <>
          <EditRecipeButton recipe={recipe} />

          <DeleteRecipeButton recipe={recipe} />
        </>
      )}
    </div>
  );
};
