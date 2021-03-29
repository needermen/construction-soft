import {Injectable} from "@angular/core";
import {BuildingMaterialCategory} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class BuildingMaterialCategoryService extends CrudService<BuildingMaterialCategory> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/buildingMaterialCategory`);
  }
}
