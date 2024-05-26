import { Home } from 'src/pages/Home';
import { Page, IPages } from './models';
import { Error } from 'src/pages/Error';
import { NotFound } from 'src/pages/NotFound';
import { Explore } from 'src/pages/Explore';
import { Saved } from 'src/pages/Saved';
import { LogIn } from 'src/pages/LogIn';

export const Pages: IPages = {
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
  [Page.Saved]: {
    paths: ['/saved'],
    component: Saved,
    public: false,
  },
};

