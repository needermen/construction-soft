import {Injectable} from "@angular/core";
import {BrigadeCategory} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";

@Injectable()
export class BrigadeCategoryService extends CrudService<BrigadeCategory> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/brigadeCategory`);
  }
}
