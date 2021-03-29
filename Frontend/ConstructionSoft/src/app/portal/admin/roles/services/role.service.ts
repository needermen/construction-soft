import {Observable} from "rxjs";
import {ListResult} from "../../../../shared/models/listResult";
import {Role} from "../models/role";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {Injectable} from "@angular/core";

@Injectable()
export class RoleService {
  url: string = `${environment.apiUrl}/role`;

  constructor(private  http: HttpClient){
  }

  get() : Observable<ListResult<Role>>{
    return this.http.get<ListResult<Role>>(this.url);
  }
}
