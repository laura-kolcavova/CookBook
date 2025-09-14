import React from 'react';
import type { FeaturedRecipe } from './models/FeaturedRecipe';
import { Link } from 'react-router-dom';
import { FaRegCircleXmark } from 'react-icons/fa6';

export type FeaturedRecipeProps = {
  recipe: FeaturedRecipe;
};

export const FeaturedRecipeCard: React.FC<FeaturedRecipeProps> = ({ recipe }) => {
  const [imgError, setImgError] = React.useState(false);

  return (
    <div key={recipe.id} className="border rounded-lg hover:shadow-lg transition-shadow">
      <Link to={`/recipes/${recipe.id}`} className="block">
        <div className=" w-full h-50 rounded-tl-lg rounded-tr-lg flex items-center justify-center bg-gray-100 rounded overflow-hidden">
          {imgError ? (
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

        <div className="p-4">
          <h3 className="text-lg font-bold mb-2">{recipe.title}</h3>
          <p className="text-color-secondary">{recipe.description}</p>
        </div>
      </Link>
    </div>
  );
};
