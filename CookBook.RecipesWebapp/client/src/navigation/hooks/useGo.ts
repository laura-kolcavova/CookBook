import { NavigateFunction, useNavigate } from 'react-router-dom';
import { IPage, IPageOptions, IParams } from '../models';
import { constructParams, getPageUrl } from 'src/utils/navigationHelpers';
import { Pages } from '../pages';
import { combineUrls } from 'src/utils/urlHelpers';

const go = (navigate: NavigateFunction) => ({
  url: (path: string, params?: IParams, options?: IPageOptions) => {
    if (/^https?:\/\//.test(path)) {
      if (options?.newTab) {
        window.open(constructParams(path, params), '_blank');
      }
      if (options?.popup) {
        window.open(
          constructParams(path, params),
          'popUpWindow',
          'height=1000,width=1000,left=10,top=10,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no,status=yes',
        );
      } else {
        window.location.href = constructParams(path, params);
      }
    } else {
      if (options?.newTab) {
        window.open(combineUrls('/', constructParams(path, params)), '_blank');
      } else {
        navigate(constructParams(path, params));
      }
    }
  },
  page: (page: IPage, options?: IPageOptions) => {
    if (options?.newTab) {
      window.open(combineUrls('/', constructParams(getPageUrl(page, options?.params))), '_blank');
    } else {
      navigate(getPageUrl(page, options?.params));
      options?.reload && window.location.reload();
    }
  },
  replace: (path: string) => {
    navigate(path, { replace: true });
  },
  back: () => {
    navigate(-1);
  },
  error: () => {
    navigate(getPageUrl(Pages.Error));
  },
});

export const useGo = () => {
  const navigate = useNavigate();

  return { go: go(navigate) };
};
