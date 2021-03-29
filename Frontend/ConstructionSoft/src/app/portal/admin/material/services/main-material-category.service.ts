import {Injectable} from "@angular/core";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";
import {MainMaterialCategory} from "../models/models";

@Injectable()
export class MainMaterialCategoryService extends CrudService<MainMaterialCategory> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/mainMaterialCategory`);
  }
}
