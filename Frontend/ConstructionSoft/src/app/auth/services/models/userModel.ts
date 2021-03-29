export class UserModel {
  fullName: string;
  id: number;
  organizationId: number;
  token: string;
  tokenExpireDate: string;
  roleIds: number[];
  passwordShouldChange: boolean
}
