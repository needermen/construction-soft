import {Observable} from "rxjs";
import {ListResult} from "../../../../shared/models/listResult";
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {Injectable} from "@angular/core";
import {Role} from "../../roles/models/role";

@Injectable()
export class UserRoleService {
  url: string = `${environment.apiUrl}/role/org`;
  organizationId: number;

  constructor(private  http: HttpClient){
  }

  setOrganization(orgId: number){
    this.organizationId = orgId;
  }

  get() : Observable<ListResult<Role>>{
    return this.http.get<ListResult<Role>>(
      this.url,
      { params: new HttpParams().set('organizationId', this.organizationId == null ? null : this.organizationId.toString())});
  }
}
