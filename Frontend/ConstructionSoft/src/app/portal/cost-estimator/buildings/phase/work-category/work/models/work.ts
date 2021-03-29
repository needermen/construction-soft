import {BuildingBase} from "../../../../models/buildingBase";

export class Work extends BuildingBase{
  id: number;
  name: string;
  order: number;

  startDate: Date;
  endDate: Date;
  DurationInDays: number;
  workCategoryId: number;
  hasToBeDoneAfterId: number;
}
