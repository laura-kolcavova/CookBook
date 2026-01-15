import { Pages } from '~/navigation/pages';
import { LinkAsButton } from '../shared/LinkAsButton';
import { LatestRecipes } from './LatestRecipes';
import { FeaturedRecipes } from './FeaturedRecipes';

export const Home = () => {
  return (
    <>
      <div className="bg-content-background-color-primary mb-10">
        <div className="container mx-auto py-20 px-4 flex flex-col items-center justify-center">
          <h1 className="text-4xl md:text-5xl font-bold text-color-primary mb-4">
            Welcome to CookBook
          </h1>

          <p className="text-lg text-text-color-secondary mb-6">
            Discover, create, and enjoy your favorite recipes.
          </p>

          <LinkAsButton to={Pages.Explore.paths[0]} className="px-6 py-3 rounded-xl shadow">
            Explore Recipes
          </LinkAsButton>
        </div>
      </div>

      <FeaturedRecipes />

      <LatestRecipes />
    </>
  );
};
