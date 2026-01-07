import { useLocation } from 'react-router-dom';

import { useCallback } from 'react';
import type { PageDefinition } from '../models';

export const useRouter = () => {
  const { pathname } = useLocation();

  const isPageActive = useCallback(
    (page: PageDefinition) => {
      return page.paths.includes(pathname);
    },
    [pathname],
  );

  return { isPageActive };
};
