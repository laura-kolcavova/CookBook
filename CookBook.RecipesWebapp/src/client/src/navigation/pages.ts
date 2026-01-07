import { Home } from '~/pages/Home';

import { NotFound } from '~/pages/NotFound';
import { Explore } from '~/pages/Explore';
import { LogIn } from '~/pages/LogIn';
import { Register } from '~/pages/Register';
import { Saved } from '~/pages/Saved';
import { MyProfile } from '~/pages/MyProfile';
import { RecipeEditor } from '~/pages/RecipeEditor';
import { Error } from '~/pages/Error';
import { MyRecipes } from '~/pages/MyRecipes';
import { RecipeDetail } from '~/pages/RecipeDetail';
import { Page, type PageDefinitions } from './models';

export const Pages: PageDefinitions = {
  [Page.Home]: {
    paths: ['/home', '/'],
    component: Home,
    public: true,
  },
  [Page.Error]: {
    paths: ['/error'],
    component: Error,
    public: true,
  },
  [Page.NotFound]: {
    paths: ['*'],
    component: NotFound,
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
    paths: ['/add-recipe'],
    component: RecipeEditor,
    public: false,
  },
  [Page.MyRecipes]: {
    paths: ['/my-recipes'],
    component: MyRecipes,
    public: false,
  },
  [Page.RecipeDetail]: {
    paths: ['/recipe/:recipeId'],
    component: RecipeDetail,
    public: false,
  },
};
