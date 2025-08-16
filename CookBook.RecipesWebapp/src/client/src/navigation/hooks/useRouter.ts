import { useNavigate } from 'react-router-dom';
import { IPage, IPageOptions } from '../models';
import { constructParams, getPageUrl } from '../../utils/navigationHelpers';
import { combineUrls } from '../../utils/urlHelpers';
import { useCallback } from 'react';

export const useRouter = () => {
  const navigate = useNavigate();

  const goToPage = useCallback(
    (page: IPage, options?: IPageOptions) => {
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

  return { goToPage };
};
