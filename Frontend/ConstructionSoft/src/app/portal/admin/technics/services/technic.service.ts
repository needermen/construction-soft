import {Injectable} from "@angular/core";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";
import {Technic} from "../models/models";

@Injectable()
export class TechnicService extends CrudService<Technic> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/technic`);
  }
}
