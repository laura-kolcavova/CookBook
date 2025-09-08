import type { PageDefinition, PageDefinitionParams } from '~/navigation/models';

export const constructParams = (path: string, params?: PageDefinitionParams) =>
  params ? `${path}?${new URLSearchParams(params).toString()}` : path;

export const replaceParams = (path: string, params?: PageDefinitionParams) => {
  let pathToGo = path;

  if (params) {
    Object.keys(params).forEach((p) => {
      pathToGo = pathToGo.replace(`:${p}`, params[p]);
    });
  }

  return pathToGo;
};

export const getPageUrl = (page: PageDefinition, params?: PageDefinitionParams) =>
  replaceParams(page.paths[0], params);
