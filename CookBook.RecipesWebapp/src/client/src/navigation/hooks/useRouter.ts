import { useLocation, useNavigate } from 'react-router-dom';

import { constructParams, getPageUrl } from '../../utils/navigationHelpers';
import { combineUrls } from '../../utils/urlHelpers';
import { useCallback } from 'react';
import { Pages } from '../pages';
import type { PageDefinition, PageDefinitionOptions } from '../models';

export const useRouter = () => {
  const navigate = useNavigate();

  const { pathname } = useLocation();

  const goToPage = useCallback(
    (page: PageDefinition, options?: PageDefinitionOptions) => {
      const pageUrl = getPageUrl(page, options?.params);

      if (options?.newTab) {
        window.open(combineUrls('/', constructParams(pageUrl)), '_blank');

        return;
      }

      navigate(pageUrl);

      if (options?.reload) {
        window.location.reload();
      }
    },
    [navigate],
  );

  const getActivePage = useCallback(() => {
    const activePage = Object.values(Pages).find((page) => page.paths.includes(pathname));

    return activePage;
  }, [pathname]);

  return { goToPage, getActivePage };
};
