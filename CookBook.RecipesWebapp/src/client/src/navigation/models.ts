import type { FC } from 'react';

export enum Page {
  Home = 'Home',
  Error = 'Error',
  NotFound = 'NotFound',
  Explore = 'Explore',
  LogIn = 'LogIn',
  Register = 'Register',
  Saved = 'Saved',
  MyProfile = 'MyProfile',
  AddRecipe = 'AddRecipe',
  MyRecipes = 'MyRecipes',
  RecipeDetail = 'RecipeDetail',
}

export type PageDefinition = {
  paths: string[];
  component: FC;
  public?: boolean;
};

export type PageDefinitionParams = {
  [key: string]: string;
};

export type PageDefinitionOptions = {
  params?: PageDefinitionParams;
  reload?: boolean;
  newTab?: boolean;
  popup?: boolean;
};

export type PageDefinitions = {
  [key in Page]: PageDefinition;
};
