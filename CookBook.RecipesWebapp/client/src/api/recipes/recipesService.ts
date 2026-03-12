import type { AxiosPromise, GenericAbortSignal } from 'axios';

import type { GetRecipeDetailResponseDto } from './dto/GetRecipeDetailResponseDto';
import type { SaveRecipeRequestDto } from './dto/SaveRecipeRequestDto';
import type { SaveRecipeResponseDto } from './dto/SaveRecipeResponseDto';
import { apiClient } from '~/api/apiClient';
import type { GetLatestRecipesResponseDto } from './dto/GetLatestRecipesResponseDto';
import type { SearchRecipesResponseDto } from './dto/SearchRecipesResponseDto';

const getLatestRecipes = (
  count: number,
  signal?: GenericAbortSignal,
): AxiosPromise<GetLatestRecipesResponseDto> => {
  return apiClient({
    url: `/api/recipes/latest`,
    method: 'GET',
    params: {
      count,
    },
    signal: signal,
  });
};

const getRecipeDetail = (
  recipeId: number,
  signal?: GenericAbortSignal,
): AxiosPromise<GetRecipeDetailResponseDto> => {
  return apiClient({
    url: `/api/recipes/${recipeId}/detail`,
    method: 'GET',
    signal: signal,
  });
};

const searchRecipes = (
  searchTerm: string | null,
  offset: number | null,
  limit: number | null,
  signal?: GenericAbortSignal,
): AxiosPromise<SearchRecipesResponseDto> => {
  return apiClient({
    url: '/api/recipes/search',
    method: 'GET',
    params: {
      searchTerm,
      offset,
      limit,
    },
    signal: signal,
  });
};

const saveRecipe = (
  saveRecipeRequest: SaveRecipeRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<SaveRecipeResponseDto> => {
  return apiClient({
    url: '/api/recipes/save',
    method: 'PUT',
    data: saveRecipeRequest,
    signal: signal,
  });
};

const removeRecipe = (
  recipeId: number,
  userName: string,
  signal?: GenericAbortSignal,
): AxiosPromise<void> => {
  return apiClient({
    url: `/api/recipes/${recipeId}/remove`,
    method: 'DELETE',
    params: {
      userName,
    },
    signal: signal,
  });
};

export const recipesService = {
  getLatestRecipes,
  getRecipeDetail,
  searchRecipes,
  saveRecipe,
  removeRecipe,
};
