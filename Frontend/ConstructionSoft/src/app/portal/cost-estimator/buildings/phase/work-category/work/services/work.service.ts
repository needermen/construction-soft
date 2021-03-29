import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Work} from "../models/work";
import {Observable} from "rxjs";
import {ListResult} from "../../../../../../../shared/models/listResult";
import {CrudService} from "../../../../../../../shared/components/crud/services/crud-service";
import {environment} from "../../../../../../../../environments/environment";

@Injectable()
export class WorkService extends CrudService<Work> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/work`);
  }

  public GetByWorkCategory(workCategoryId): Observable<ListResult<Work>> {
    return this.http.get<ListResult<Work>>(`${environment.apiUrl}/workCategory/${workCategoryId}/work`);
  }
}
