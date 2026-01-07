import { atomWithStorage, createJSONStorage } from 'jotai/utils';
import type { User } from '~/authentication/accounts/user';
import { LOCAL_STORAGE_USER } from '~/constants';

const storage = createJSONStorage<User>(() => localStorage);

export const EMPTY_USER: User = {
  isAuthenticated: false,
  nameClaimType: '',
  roleClaimType: '',
  emailClaimType: '',
  claims: [],
};

export const userAtom = atomWithStorage<User>(LOCAL_STORAGE_USER, EMPTY_USER, storage, {
  getOnInit: true,
});
