import { useRef, useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { messages } from './messages';
import type { RecipesSearchGridRef } from './shared/RecipesSearchGrid';
import { RecipesSearchGrid } from './shared/RecipesSearchGrid';
import { SearchBar } from './shared/SearchBar';

export const Explore = () => {
  const recipesSearchGridRef = useRef<RecipesSearchGridRef>(null);

  const [activeSearchTerm, setActiveSearchTerm] = useState('');

  const handleSearch = (searchTerm: string) => {
    if (activeSearchTerm === searchTerm) {
      recipesSearchGridRef.current?.reload();
    } else {
      setActiveSearchTerm(searchTerm);
    }
  };

  return (
    <>
      <div className="bg-content-background-color-primary mb-4">
        <div className="page-container mx-auto py-10 px-4">
          <div className="max-w-2xl mx-auto">
            <h1 className="text-2xl font-semibold text-text-color-primary mb-8 text-center">
              <FormattedMessage {...messages.exploreRecipesTitle} />
            </h1>

            <SearchBar onSearch={handleSearch} />
          </div>
        </div>
      </div>

      <div className="bg-content-background-color-tertiary">
        <div className="page-container mx-auto py-10 px-4">
          <RecipesSearchGrid ref={recipesSearchGridRef} searchTerm={activeSearchTerm} />
        </div>
      </div>
    </>
  );
};
