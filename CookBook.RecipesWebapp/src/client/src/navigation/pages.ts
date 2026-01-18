import { Home } from '~/pages/Home';

import { Explore } from '~/pages/Explore';
import { LogIn } from '~/pages/LogIn';
import { Register } from '~/pages/Register';
import { Saved } from '~/pages/Saved';
import { MyProfile } from '~/pages/MyProfile';
import { RecipeEditor } from '~/pages/RecipeEditor';
import { MyRecipes } from '~/pages/MyRecipes';
import { RecipeDetail } from '~/pages/RecipeDetail';
import type { PageDefinition } from './PageDefinition';
import { Page } from './Page';
import { NotFound } from '~/pages/NotFound';

type PageDefinitions = {
  [key in Page]: PageDefinition;
};

export const pages: PageDefinitions = {
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
    paths: ['/recipes/:recipeId'],
    component: RecipeDetail,
    public: false,
  },
  [Page.NotFound]: {
    paths: ['*'],
    component: NotFound,
    public: true,
  },
};
