import React from 'react';

import { Pages } from '~/navigation/pages';
import { LinkAsButton } from '~/sharedComponents/LinkAsButton';

export const Home: React.FC = () => {
  return (
    <div className="content-background-color-primary">
      <div className="container mx-auto text-center py-20">
        <h1 className="text-4xl md:text-5xl font-bold mb-4">Welcome to CookBook</h1>

        <p className="text-lg text-gray-700 mb-6">
          Discover, create, and enjoy your favorite recipes.
        </p>

        <LinkAsButton to={Pages.Explore.paths[0]} className="px-6 py-3 rounded-xl shadow">
          Explore Recipes
        </LinkAsButton>
      </div>
    </div>
  );
};
