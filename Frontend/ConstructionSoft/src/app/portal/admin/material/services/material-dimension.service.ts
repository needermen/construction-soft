import {Injectable} from "@angular/core";
import {MaterialDimension} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class MaterialDimensionService extends CrudService<MaterialDimension> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/materialDimension`);
  }
}
