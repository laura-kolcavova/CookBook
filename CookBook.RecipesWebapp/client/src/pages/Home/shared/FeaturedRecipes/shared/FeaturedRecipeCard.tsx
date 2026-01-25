import React from 'react';

import { Link } from 'react-router-dom';
import { FaRegCircleXmark } from 'react-icons/fa6';
import type { FeaturedRecipe } from '../models/FeaturedRecipe';
import { pages } from '~/navigation/pages';
import { FormattedMessage } from 'react-intl';
import { sharedMessages } from '~/pages/shared/sharedMessages';

export type FeaturedRecipeProps = {
  recipe: FeaturedRecipe;
};

export const FeaturedRecipeCard: React.FC<FeaturedRecipeProps> = ({ recipe }) => {
  const [imgError, setImgError] = React.useState(false);

  const recipeDetailPath = pages.RecipeDetail.paths[0].replace(':recipeId', recipe.id.toString());

  return (
    <div
      key={recipe.id}
      className="border rounded-lg hover:shadow-lg transition-shadow bg-gray-100">
      <Link to={recipeDetailPath} className="block">
        <div className="w-full h-50 rounded rounded-tl-lg rounded-tr-lg flex items-center justify-center overflow-hidden">
          {imgError ? (
            <span className="flex flex-col items-center justify-center w-full h-full text-gray-400">
              <FaRegCircleXmark size="2.5rem" />
              <FormattedMessage {...sharedMessages.notFound} />
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

        <div className="p-4">
          <h3 className="text-lg font-bold mb-2 text-center">{recipe.title}</h3>
        </div>
      </Link>
    </div>
  );
};
