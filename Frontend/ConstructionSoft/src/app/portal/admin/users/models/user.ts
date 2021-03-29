import {Organization} from "../../organizations/models/organization";

export class User {
  id: number;
  fullName: string;
  userName: string;
  phoneNumber: string;
  personalId: string;
  active: boolean;

  organizationId: number;
  organization: Organization;

  roleIds: number[]
}
