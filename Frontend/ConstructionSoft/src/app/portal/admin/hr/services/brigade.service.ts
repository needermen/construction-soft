import {Injectable} from "@angular/core";
import {Brigade} from "../models/models";
import {environment} from "../../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";

@Injectable()
export class BrigadeService extends CrudService<Brigade> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/brigade`);
  }
}
