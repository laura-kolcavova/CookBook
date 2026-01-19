import { useState } from 'react';
import { FaSearch } from 'react-icons/fa';
import { useSearchRecipesQuery } from './hooks/useSearchRecipesQuery';
import { LoadingSpinner } from '~/pages/shared/LoadingSpinner';
import { Alert } from '~/pages/shared/Alert';
import { LatestRecipeCard } from '../Home/LatestRecipes/shared/LatestRecipeCard';
import { FormTextInput } from '../shared/forms/FormTextInput';
import { Button } from '../shared/Button';

export const Explore = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [activeSearch, setActiveSearch] = useState('');

  const { isLoading, isError, data } = useSearchRecipesQuery(activeSearch);

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    setActiveSearch(searchTerm);
  };

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-2xl font-bold text-text-color-primary mb-8 text-center">
            Explore Recipes
          </h1>

          <form onSubmit={handleSearch} className="mb-10">
            <div className="flex">
              <div className="flex-1">
                <FormTextInput
                  type="text"
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  placeholder="Search for recipes by title, description, or tags..."
                  className="h-10 rounded-tl-md rounded-bl-md rounded-tr-none rounded-br-none"
                />
              </div>
              <Button
                type="submit"
                className="h-10 rounded-bl-none rounded-tl-none rounded-br-md rounded-tr-md px-6 flex items-center gap-2">
                <FaSearch />
                Search
              </Button>
            </div>
          </form>

          {!activeSearch ? null : isLoading ? (
            <div className="flex items-center justify-center py-20">
              <LoadingSpinner text="Searching..." />
            </div>
          ) : isError ? (
            <Alert color="danger">Something went wrong while searching for recipes</Alert>
          ) : !data || data.recipes.length === 0 ? (
            <Alert color="warning">
              <span>No recipes found matching {activeSearch}</span>
            </Alert>
          ) : (
            <div>
              <div className="mb-6">
                <p className="text-text-color-secondary">
                  Found {data.totalCount} recipe{data.totalCount !== 1 ? 's' : ''} matching{' '}
                  {activeSearch}
                </p>
              </div>

              <div className="flex flex-col gap-6">
                {data.recipes.map((recipe) => (
                  <LatestRecipeCard key={recipe.id} recipe={recipe} />
                ))}
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};
