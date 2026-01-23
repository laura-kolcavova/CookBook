import { Home } from '~/pages/Home';

import { Explore } from '~/pages/Explore';
import { LogIn } from '~/pages/LogIn';
import { Register } from '~/pages/Register';
import { Saved } from '~/pages/Saved';
import { MyProfile } from '~/pages/MyProfile';
import { MyRecipes } from '~/pages/MyRecipes';
import { RecipeDetail } from '~/pages/RecipeDetail';
import { Page } from './Page';
import { NotFound } from '~/pages/NotFound';
import { AddRecipe } from '~/pages/AddRecipe';
import { EditRecipe } from '~/pages/EditRecipe';

export const pages = {
  [Page.Home]: {
    paths: ['/home', '/'],
    component: Home,
    public: true,
  },
  [Page.Explore]: {
    paths: ['/explore'],
    component: Explore,
    public: true,
  },
  [Page.LogIn]: {
    paths: ['/login'],
    component: LogIn,
    public: true,
  },
  [Page.Register]: {
    paths: ['/register'],
    component: Register,
    public: true,
  },
  [Page.Saved]: {
    paths: ['/saved'],
    component: Saved,
    public: false,
  },
  [Page.MyProfile]: {
    paths: ['/my-profile'],
    component: MyProfile,
    public: false,
  },
  [Page.AddRecipe]: {
    paths: ['/recipes/add'],
    component: AddRecipe,
    public: false,
  },
  [Page.EditRecipe]: {
    paths: ['/recipes/:recipeId/edit'],
    component: EditRecipe,
    public: false,
  },
  [Page.MyRecipes]: {
    paths: ['/my-recipes'],
    component: MyRecipes,
    public: false,
  },
  [Page.RecipeDetail]: {
    paths: ['/recipes/:recipeId/detail'],
    component: RecipeDetail,
    public: false,
  },
  [Page.NotFound]: {
    paths: ['*'],
    component: NotFound,
    public: true,
  },
};
