import {Injectable} from "@angular/core";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {HttpClient} from "@angular/common/http";
import {User} from "../models/user";

@Injectable()
export class UserService extends CrudService<User> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/user`);
  }
}
