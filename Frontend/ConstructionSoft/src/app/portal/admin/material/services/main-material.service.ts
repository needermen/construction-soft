import {Injectable} from "@angular/core";
import {MainMaterial} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class MainMaterialService extends CrudService<MainMaterial> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/mainMaterial`);
  }
}
