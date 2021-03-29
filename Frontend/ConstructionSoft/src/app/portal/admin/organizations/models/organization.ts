export class Organization {
  id: number;
  name: string;
  taxCode: string;
  phoneNumber: string;
  legalAddress: string;
  actualAddress: string;
  bank : string;
  bankCode : string;
  accountNumber : string;
  agreementNumber : string;
  agreementDate: Date;
  director : string;
  directorPhoneNumber : string;
  active: boolean;

  roleIds: number[];
}
