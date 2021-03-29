import {Injectable} from "@angular/core";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";
import {Organization} from "../models/organization";

@Injectable()
export class OrganizationService extends CrudService<Organization> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/organization`);
  }
}
