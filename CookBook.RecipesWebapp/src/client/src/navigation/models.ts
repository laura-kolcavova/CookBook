import { FC } from 'react';

export type IPage = {
  paths: string[];
  component: FC;
  public?: boolean;
};

export interface IParams {
  [key: string]: string;
}

export interface IPageOptions {
  params?: IParams;
  reload?: boolean;
  newTab?: boolean;
  popup?: boolean;
}

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
}

export type IPages = {
  [key in Page]: IPage;
};
