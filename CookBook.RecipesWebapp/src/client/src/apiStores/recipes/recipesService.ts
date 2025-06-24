import { AxiosPromise, GenericAbortSignal } from 'axios';
import { httpClient } from '~/services/httpClient';
import { SaveRecipeResponseDto } from './models/SaveRecipeResponseDto';
import { SaveRecipeRequestDto } from './models/SaveRecipeRequestDto';

const saveRecipe = (
  request: SaveRecipeRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<SaveRecipeResponseDto> => {
  return httpClient({
    url: '/api/Recipes/Save',
    method: 'POST',
    data: request,
    signal: signal,
  });
};

const RecipesService = {
  saveRecipe,
};

export default RecipesService;
