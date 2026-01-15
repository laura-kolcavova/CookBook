import { useState } from 'react';
import { FaRegCircleXmark } from 'react-icons/fa6';
import { Link } from 'react-router-dom';
import { LatestRecipeDto } from '~/api/recipes/dto/LatestRecipeDto';
import { Pages } from '~/navigation/pages';

type LatestRecipeCardProps = {
  recipe: LatestRecipeDto;
};

export const LatestRecipeCard = ({ recipe }: LatestRecipeCardProps) => {
  const [imgError, setImgError] = useState(false);

  const recipeDetailPath = Pages.RecipeDetail.paths[0].replace(':recipeId', recipe.id.toString());

  return (
    <div className="grid grid-cols-[1fr_2fr] max-w-200">
      <div className="rounded rounded-tl-lg rounded-tr-lg bg-gray-100 overflow-hidden">
        <Link to={recipeDetailPath} className="block">
          <div className="aspect-4/5 flex items-center justify-center">
            {!recipe.imageUrl || imgError ? (
              <span className="flex flex-col items-center justify-center w-full h-full text-gray-400">
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
        <h3 className="text-lg font-bold mb-2">{recipe.title}</h3>
        <p className="text-color-secondary">{recipe.description}</p>
      </div>
    </div>
  );
};
