import {Injectable} from "@angular/core";
import {WorkerCategory} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";
@Injectable()
export class WorkerCategoryService extends CrudService<WorkerCategory> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/workerCategory`);
  }
}
