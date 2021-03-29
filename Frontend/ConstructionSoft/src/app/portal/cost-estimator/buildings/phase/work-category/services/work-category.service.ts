import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {WorkCategory} from "../models/work-category";
import {Observable} from "rxjs";
import {ListResult} from "../../../../../../shared/models/listResult";
import {CrudService} from "../../../../../../shared/components/crud/services/crud-service";
import {environment} from "../../../../../../../environments/environment";

@Injectable()
export class WorkCategoryService extends CrudService<WorkCategory> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/workCategory`);
  }

  public GetByPhase(phaseId): Observable<ListResult<WorkCategory>> {
    return this.http.get<ListResult<WorkCategory>>(`${environment.apiUrl}/phase/${phaseId}/workCategory`);
  }
}
