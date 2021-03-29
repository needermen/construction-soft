import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Phase} from "../models/phase";
import {CrudService} from "../../../../../shared/components/crud/services/crud-service";
import {environment} from "../../../../../../environments/environment";
import {Observable} from "rxjs";
import {ListResult} from "../../../../../shared/models/listResult";

@Injectable()
export class PhaseService extends CrudService<Phase> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/phase`);
  }

  public GetByBuilding(buildingId): Observable<ListResult<Phase>> {
    return this.http.get<ListResult<Phase>>(`${environment.apiUrl}/building/${buildingId}/phase`);
  }
}
