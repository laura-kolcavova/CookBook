import { useState } from 'react';
import { FaSearch, FaTimes } from 'react-icons/fa';

export type SearchBarProps = {
  onSearch: (searchTerm: string) => void;
};

export const SearchBar = ({ onSearch }: SearchBarProps) => {
  const [searchTerm, setSearchTerm] = useState('');

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch(searchTerm);
  };

  const handleClear = () => {
    setSearchTerm('');
  };

  return (
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
  );
};
