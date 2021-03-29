import {Injectable} from "@angular/core";
import {ConsumptionMaterial} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class ConsumptionMaterialService extends CrudService<ConsumptionMaterial> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/consumptionMaterial`);
  }
}
