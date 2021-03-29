import {BuildingStatus } from "./buildingStatus";

export class Building {
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
  agreementDate?: Date;
  director : string;
  directorPhoneNumber : string;
  email: string;

  status: BuildingStatus;
  statusName: string;
}
