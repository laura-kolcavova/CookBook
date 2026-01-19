import { useState } from 'react';
import { FaRegCircleXmark } from 'react-icons/fa6';
import { Link } from 'react-router-dom';
import type { RecipeSearchItemDto } from '~/api/recipes/dto/RecipeSearchItemDto';
import { useFormatting } from '~/localization/useFormatting';
import { pages } from '~/navigation/pages';

export type RecipeSearchItemCardProps = {
  recipe: RecipeSearchItemDto;
};

export const RecipeSearchItemCard = ({ recipe }: RecipeSearchItemCardProps) => {
  const [imgError, setImgError] = useState(false);

  const { formatDate } = useFormatting();

  const recipeDetailPath = pages.RecipeDetail.paths[0].replace(
    ':recipeId',
    recipe.recipeId.toString(),
  );

  return (
    <div className="grid grid-cols-[1fr_2fr] md:grid-cols-[1fr_2.5fr] max-w-200">
      <div className="rounded rounded-tl-lg rounded-tr-lgoverflow-hidden hover:shadow-lg transition-shadow">
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

      <div className="p-8">
        <h3 className="mb-2">
          <Link
            to={recipeDetailPath}
            className="text-xl font-bold text-text-color-primary hover:text-text-color-primary-highlight transition-colors">
            {recipe.title}
          </Link>
        </h3>

        <div className="mb-2">
          <span className="text-sm text-text-color-tertiary">{formatDate(recipe.createdAt)}</span>
        </div>
      </div>
    </div>
  );
};
