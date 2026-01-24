import { FormattedMessage } from 'react-intl';
import type { FeaturedRecipe } from './models/FeaturedRecipe';
import { FeaturedRecipeCard } from './shared/FeaturedRecipeCard';
import { messages } from '../messages';

// Dummy featured recipes data for demonstration
const featuredRecipes: FeaturedRecipe[] = [
  {
    id: 1,
    title: 'Classic Lasagna',
    imageUrl: '/public/lasagna.jpg',
  },
  {
    id: 2,
    title: 'Vegan Buddha Bowl',
    imageUrl: '/public/buddha-bowl.jpg',
  },
  {
    id: 3,
    title: 'Chocolate Chip Cookies',
    imageUrl: '/public/cookies.jpg',
  },
];

export const FeaturedRecipes = () => {
  return (
    <div className="bg-content-background-color-secondary">
      <div className="container mx-auto py-10 px-4">
        <h2 className="text-3xl mb-6 text-center font-handwritten">
          <FormattedMessage {...messages.featuredRecipesTitle} />
        </h2>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {featuredRecipes.map((recipe) => (
            <FeaturedRecipeCard recipe={recipe} key={recipe.id} />
          ))}
        </div>
      </div>
    </div>
  );
};
