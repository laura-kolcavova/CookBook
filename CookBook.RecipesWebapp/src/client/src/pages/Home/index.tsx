import React from 'react';

import { Pages } from '~/navigation/pages';
import { LinkAsButton } from '~/sharedComponents/LinkAsButton';
import { FeaturedRecipeCard } from './FeaturedRecipeCard';
import type { FeaturedRecipe } from './models/FeaturedRecipe';

// Dummy featured recipes data for demonstration
const featuredRecipes: FeaturedRecipe[] = [
  {
    id: 1,
    title: 'Classic Lasagna',
    description: 'Layers of pasta, cheese, and rich meat sauce.',
    imageUrl: '/public/lasagna.jpg',
  },
  {
    id: 2,
    title: 'Vegan Buddha Bowl',
    description: 'A nourishing bowl packed with veggies and grains.',
    imageUrl: '/public/buddha-bowl.jpg',
  },
  {
    id: 3,
    title: 'Chocolate Chip Cookies',
    description: 'Crispy on the outside, chewy on the inside.',
    imageUrl: '/public/cookies.jpg',
  },
];

export const Home: React.FC = () => {
  return (
    <>
      <div className="content-background-color-primary mb-10">
        <div className="container mx-auto py-20 text-center">
          <h1 className="text-4xl md:text-5xl font-bold mb-4">Welcome to CookBook</h1>

          <p className="text-lg text-gray-700 mb-6">
            Discover, create, and enjoy your favorite recipes.
          </p>

          <LinkAsButton to={Pages.Explore.paths[0]} className="px-6 py-3 rounded-xl shadow">
            Explore Recipes
          </LinkAsButton>
        </div>
      </div>

      <div className="content-background-color-tertiary">
        <div className="container mx-auto py-10">
          <h2 className="text-2xl font-semibold mb-6 text-left">Featured Recipes</h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            {featuredRecipes.map((recipe) => (
              <FeaturedRecipeCard recipe={recipe} key={recipe.id} />
            ))}
          </div>
        </div>
      </div>
    </>
  );
};
