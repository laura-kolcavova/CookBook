import { AxiosPromise } from 'axios';
import { httpClient } from '~/services/httpClient';
import { SaveRecipeRequest } from './models/SaveRecipeRequest';

const saveRecipe = (request: SaveRecipeRequest): AxiosPromise<void> => {
  return httpClient({
    url: '/api/sequences',
    method: 'PUT',
    data: request,
  });
};

const RecipesService = {
  saveRecipe,
};

export default RecipesService;
