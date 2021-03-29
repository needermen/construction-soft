import {Injectable} from "@angular/core";
import {Worker} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class WorkerService extends CrudService<Worker> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/worker`);
  }
}
