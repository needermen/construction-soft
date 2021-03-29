import {Injectable} from "@angular/core";
import {ConsumptionMaterialCategory} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class ConsumptionMaterialCategoryService extends CrudService<ConsumptionMaterialCategory> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/consumptionMaterialCategory`);
  }
}
