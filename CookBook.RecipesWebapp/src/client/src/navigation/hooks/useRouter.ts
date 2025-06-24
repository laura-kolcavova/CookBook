import { useLocation, useNavigate } from 'react-router-dom';
import { IPage, IPageOptions } from '../models';
import { constructParams, getPageUrl } from '../../utils/navigationHelpers';
import { combineUrls } from '../../utils/urlHelpers';
import { Pages } from '../pages';

export const useRouter = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const goToPage = (page: IPage, options?: IPageOptions) => {
    const pageUrl = getPageUrl(page, options?.params);

    if (options?.newTab) {
      window.open(combineUrls('/', constructParams(pageUrl)), '_blank');
    } else {
      navigate(pageUrl);
      options?.reload && window.location.reload();
    }
  };

  const currentPage = (): IPage | undefined => {
    const pages = Object.values(Pages);

    const currentPage = pages.find(
      (page) =>
        page.paths.length > 0 &&
        page.paths.some(
          (path) =>
            location.pathname.localeCompare(path, undefined, { sensitivity: 'accent' }) === 0,
        ),
    );

    return currentPage;
  };

  return { goToPage, currentPage };
};
