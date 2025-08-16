import { AxiosPromise, GenericAbortSignal } from 'axios';
import { httpClient } from '~/services/httpClient';
import { SaveRecipeResponseDto } from './models/SaveRecipeResponseDto';
import { SaveRecipeRequestDto } from './models/SaveRecipeRequestDto';
import { GetRecipeDetailResponseDto } from './models/GetRecipeDetailResponseDto';

const saveRecipe = (
  saveRecipeRequest: SaveRecipeRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<SaveRecipeResponseDto> => {
  return httpClient({
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
  return httpClient({
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
