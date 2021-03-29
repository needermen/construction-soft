import {BuildingBase} from "../../../models/buildingBase";

export class WorkCategory extends BuildingBase{
  id: number;
  name: string;
  order: number;
  phaseId: number;
}
