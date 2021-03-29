import {Injectable} from "@angular/core";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";
import {TechnicDimension} from "../models/models";

@Injectable()
export class TechnicDimensionService extends CrudService<TechnicDimension> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/technicDimension`);
  }
}
