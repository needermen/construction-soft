import {BuildingBase} from "../../models/buildingBase";

export class Phase extends BuildingBase {
  id: number;
  name: string;
  order: number;
  buildingId: number;
}
