import { IPage, IParams } from '../navigation/models';

export const constructParams = (path: string, params?: IParams) =>
  params ? `${path}?${new URLSearchParams(params).toString()}` : path;

export const replaceParams = (path: string, params?: IParams) => {
  let pathToGo = path;

  if (params) {
    Object.keys(params).forEach((p) => {
      pathToGo = pathToGo.replace(`:${p}`, params[p]);
    });
  }

  return pathToGo;
};

export const getPageUrl = (page: IPage, params?: IParams) => replaceParams(page.paths[0], params);
