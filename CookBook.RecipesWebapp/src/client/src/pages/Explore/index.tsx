import { useState } from 'react';
import { FaSearch, FaTimes } from 'react-icons/fa';
import { useSearchRecipesQuery } from './hooks/useSearchRecipesQuery';
import { LoadingSpinner } from '~/pages/shared/LoadingSpinner';
import { Alert } from '~/pages/shared/Alert';
import { LatestRecipeCard } from '../Home/LatestRecipes/shared/LatestRecipeCard';

export const Explore = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [activeSearch, setActiveSearch] = useState('');

  const { isLoading, isError, data } = useSearchRecipesQuery(activeSearch);

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    setActiveSearch(searchTerm);
  };

  const handleClear = () => {
    setSearchTerm('');
  };

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        <div className="max-w-2xl mx-auto">
          <h1 className="text-2xl font-semibold text-text-color-primary mb-8 text-center">
            Explore Recipes
          </h1>

          <form onSubmit={handleSearch} className="mb-10">
            <div className="flex flex-row items-center">
              <div className="flex-1 relative mx-[1px]">
                <div className="absolute left-3 top-1/2 -translate-y-1/2 text-text-color-tertiary">
                  <FaSearch />
                </div>

                <input
                  type="text"
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  placeholder="Search for recipes by title, description, or tags..."
                  className="block w-full py-1.5 outline-1 outline-offset-1 outline-gray-300 bg-form-text-input-background-color text-form-text-input-color text-sm h-[calc(2.5rem-3px)] px-10 rounded-tl-md rounded-bl-md rounded-tr-none rounded-br-none"
                />

                {searchTerm && (
                  <button
                    type="button"
                    onClick={handleClear}
                    className="absolute right-3 top-1/2 -translate-y-1/2 text-text-color-tertiary hover:text-text-color-secondary transition-colors cursor-pointer">
                    <FaTimes />
                  </button>
                )}
              </div>

              <button
                type="submit"
                className="py-2 font-normal transition-colors duration-150 cursor-pointer bg-button-background-color-primary text-button-color-primary hover:bg-button-background-color-primary-hover disabled:bg-button-background-color-primary-disabled px-6 h-10 rounded-bl-none rounded-tl-none rounded-br-md rounded-tr-md">
                Search
              </button>
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
