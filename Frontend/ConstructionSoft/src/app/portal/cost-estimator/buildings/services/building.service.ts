import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {Building} from "../models/building";
import {BuildingBase} from "../models/buildingBase";
import {Observable} from "rxjs";
import {ListResult} from "../../../../shared/models/listResult";
import {DateRange} from "../models/dateRange";

@Injectable()
export class BuildingService extends CrudService<Building> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/building`);
  }

  public GetBaseBuildingDetails(id: number, url: string): Observable<BuildingBase>{
    return this.http.get<BuildingBase>(`${environment.apiUrl}/${url}/${id}`);
  }

  public GetDateRanges(id: number, url: string) : Observable<ListResult<DateRange>>{
    return this.http.get<ListResult<DateRange>>(`${environment.apiUrl}/${url}/${id}/dates`);
  }
}
