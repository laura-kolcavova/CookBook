import type { Claim } from './claim';

export type User = {
  isAuthenticated: boolean;
  nameClaimType: string;
  roleClaimType: string;
  emailClaimType: string;
  claims: Claim[];
};
