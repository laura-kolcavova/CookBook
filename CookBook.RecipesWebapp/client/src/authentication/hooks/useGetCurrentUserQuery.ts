import { useQuery } from '@tanstack/react-query';
import { usersService } from '~/api/users/usersService';

export const useGetCurrentUserQuery = () => {
  return useQuery({
    queryKey: ['getCurrentUser'],
    queryFn: async ({ signal }) => {
      const { data } = await usersService.getCurrentUser(signal);

      return data;
    },
    retry: 0,
    gcTime: 0,
    refetchOnWindowFocus: false,
    throwOnError: true,
  });
};
