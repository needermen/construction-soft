import {Injectable} from "@angular/core";
import {BuildingMaterial} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class BuildingMaterialService extends CrudService<BuildingMaterial> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/buildingMaterial`);
  }
}
