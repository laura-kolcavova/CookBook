import type { AxiosPromise, GenericAbortSignal } from 'axios';

import type { GetRecipeDetailResponseDto } from './models/GetRecipeDetailResponseDto';
import type { SaveRecipeRequestDto } from './models/SaveRecipeRequestDto';
import type { SaveRecipeResponseDto } from './models/SaveRecipeResponseDto';
import { callAxios } from '~/utils/axios';

const saveRecipe = (
  saveRecipeRequest: SaveRecipeRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<SaveRecipeResponseDto> => {
  return callAxios({
    url: '/api/Recipes/Save',
    method: 'POST',
    data: saveRecipeRequest,
    signal: signal,
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

const RecipesService = {
  saveRecipe,
  getRecipeDetail,
};

export default RecipesService;
