import { Claim } from './claim';

export type User = {
  isAuthenticated: boolean;
  nameClaimType: string;
  roleClaimType: string;
  emailClaimType: string;
  claims: Claim[];
};

export const EMPTY_USER: User = {
  isAuthenticated: false,
  nameClaimType: '',
  roleClaimType: '',
  emailClaimType: '',
  claims: [],
};

