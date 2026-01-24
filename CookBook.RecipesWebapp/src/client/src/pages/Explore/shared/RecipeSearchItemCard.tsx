import { useState } from 'react';
import { FaRegCircleXmark } from 'react-icons/fa6';
import { useIntl } from 'react-intl';
import { Link } from 'react-router-dom';
import type { RecipeSearchItemDto } from '~/api/recipes/dto/RecipeSearchItemDto';
import { pages } from '~/navigation/pages';

export type RecipeSearchItemCardProps = {
  recipe: RecipeSearchItemDto;
};

export const RecipeSearchItemCard = ({ recipe }: RecipeSearchItemCardProps) => {
  const [imgError, setImgError] = useState(false);

  const { formatDate } = useIntl();

  const recipeDetailPath = pages.RecipeDetail.paths[0].replace(
    ':recipeId',
    recipe.recipeId.toString(),
  );

  return (
    <div className="flex flex-col">
      <div className="rounded rounded-tl-lg rounded-tr-lgoverflow-hidden hover:shadow-lg transition-shadow mb-2">
        <Link to={recipeDetailPath} className="block">
          <div className="aspect-4/5 flex items-center justify-center">
            {!recipe.imageUrl || imgError ? (
              <span className="flex flex-col items-center justify-center w-full h-full bg-gray-100 text-gray-400">
                <FaRegCircleXmark size="2.5rem" />
                <span>Not Found</span>
              </span>
            ) : (
              <img
                src={recipe.imageUrl}
                alt={recipe.title}
                className="object-cover w-full h-full"
                onError={() => {
                  setImgError(true);
                }}
              />
            )}
          </div>
        </Link>
      </div>

      <div className="mb-1 text-center">
        <Link
          to={recipeDetailPath}
          className=" text-text-color-primary hover:text-text-color-primary-highlight transition-colors">
          <h3 className="text-lg font-bold wrap-break-word">{recipe.title}</h3>
        </Link>
      </div>

      <div className="text-center">
        <span className="text-xs text-text-color-tertiary">{formatDate(recipe.createdAt)}</span>
      </div>
    </div>
  );
};
