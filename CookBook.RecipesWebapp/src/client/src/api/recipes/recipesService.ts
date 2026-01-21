import type { AxiosPromise, GenericAbortSignal } from 'axios';

import type { GetRecipeDetailResponseDto } from './dto/GetRecipeDetailResponseDto';
import type { SaveRecipeRequestDto } from './dto/SaveRecipeRequestDto';
import type { SaveRecipeResponseDto } from './dto/SaveRecipeResponseDto';
import { callAxios } from '~/utils/axios';
import type { GetLatestRecipesResponseDto } from './dto/GetLatestRecipesResponseDto';
import type { SearchRecipesResponseDto } from './dto/SearchRecipesResponseDto';

const saveRecipe = (
  saveRecipeRequest: SaveRecipeRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<SaveRecipeResponseDto> => {
  return callAxios({
    url: '/api/Recipes/Save',
    method: 'PUT',
    data: saveRecipeRequest,
    signal: signal,
  });
};

const getLatestRecipes = (
  count: number,
  signal?: GenericAbortSignal,
): AxiosPromise<GetLatestRecipesResponseDto> => {
  return callAxios({
    url: `/api/Recipes/Latest`,
    method: 'GET',
    signal: signal,
    params: {
      count,
    },
  });
};

const getRecipeDetail = (
  recipeId: number,
  signal?: GenericAbortSignal,
): AxiosPromise<GetRecipeDetailResponseDto> => {
  return callAxios({
    url: `/api/Recipes/${recipeId}/Detail`,
    method: 'GET',
    signal: signal,
  });
};

const searchRecipes = (
  searchTerm?: string,
  offset?: number,
  limit?: number,
  signal?: GenericAbortSignal,
): AxiosPromise<SearchRecipesResponseDto> => {
  return callAxios({
    url: '/api/Recipes/Search',
    method: 'GET',
    signal: signal,
    params: {
      searchTerm,
      offset,
      limit,
    },
  });
};

export const recipesService = {
  saveRecipe,
  getLatestRecipes,
  getRecipeDetail,
  searchRecipes,
};
