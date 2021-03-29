import {environment} from "../../../../../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ListResult} from "../../../../../../../shared/models/listResult";
import {Injectable} from "@angular/core";
import {Work} from "../models/work";
import {map} from "rxjs/operators";

@Injectable()
export class HasToBeDoneAfterWorkService {
  url: string;
  workId: number;
  workCategoryId: number;


  constructor(private http: HttpClient){
  }

  setWorkId(workId: number){
    this.workId = workId;
  }

  setWorkCategoryId(workCategoryId: number){
    this.workCategoryId = workCategoryId;
    this.url = `${environment.apiUrl}/workCategory/${this.workCategoryId}/work`;
  }

  get() : Observable<ListResult<Work>>{
    let workid = this.workId;

    return this.http.get<ListResult<Work>>(this.url).pipe(map((result) => {
      result.items = result.items.filter(function (element) {
        return element.id != workid;
      });

      return result;
    }));
  }
}
