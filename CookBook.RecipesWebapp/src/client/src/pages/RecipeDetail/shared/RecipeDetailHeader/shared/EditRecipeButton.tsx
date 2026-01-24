import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { FormattedMessage } from 'react-intl';
import { Button } from '~/pages/shared/Button';
import { useNavigate } from 'react-router-dom';
import { pages } from '~/navigation/pages';
import { messages } from '~/pages/RecipeDetail/messages';

export type EditRecipeButtonProps = {
  recipe: RecipeDetailDto;
};

export const EditRecipeButton = ({ recipe }: EditRecipeButtonProps) => {
  const navigate = useNavigate();

  const redirectToRecipeEditor = () => {
    const editRecipePath = pages.EditRecipe.paths[0].replace(
      ':recipeId',
      recipe.recipeId.toString(),
    );

    navigate(editRecipePath);
  };

  return (
    <Button onClick={redirectToRecipeEditor} variant="primary">
      <FormattedMessage {...messages.editButton} />
    </Button>
  );
};
