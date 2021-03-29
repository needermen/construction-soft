import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {CrudService} from "../../../../../../../../../shared/components/crud/services/crud-service";
import {environment} from "../../../../../../../../../../environments/environment";

@Injectable()
export class WorkBuildingMaterialService extends CrudService<any> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/work/0/buildingMaterial`);
  }

  setWorkId(workId: number){
    this.url = `${environment.apiUrl}/work/${workId}/buildingMaterial`
  }
}
