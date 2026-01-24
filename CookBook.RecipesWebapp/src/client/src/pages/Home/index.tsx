import { FormattedMessage } from 'react-intl';
import { LinkAsButton } from '../shared/LinkAsButton';
import { LatestRecipes } from './LatestRecipes';
import { FeaturedRecipes } from './FeaturedRecipes';
import { pages } from '~/navigation/pages';
import { messages } from './messages';

export const Home = () => {
  return (
    <>
      <div className="bg-content-background-color-primary mb-10">
        <div className="container mx-auto py-20 px-4 flex flex-col items-center justify-center">
          <h1 className="text-4xl md:text-5xl font-bold text-text-color-primary mb-4">
            <FormattedMessage {...messages.welcomeTitle} />
          </h1>

          <p className="text-lg text-text-color-secondary mb-6">
            <FormattedMessage {...messages.welcomeDescription} />
          </p>

          <LinkAsButton to={pages.Explore.paths[0]} className="px-6 py-3 rounded-xl shadow">
            <FormattedMessage {...messages.exploreRecipesButton} />
          </LinkAsButton>
        </div>
      </div>

      <FeaturedRecipes />

      <LatestRecipes />
    </>
  );
};
