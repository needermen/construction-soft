import {Injectable} from "@angular/core";
import {CrudService} from "../../../../shared/components/crud/services/crud-service";
import {PaymentType} from "../models/models";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";

@Injectable()
export class PaymentTypeService extends CrudService<PaymentType> {
  constructor(public http: HttpClient){
    super(http, `${environment.apiUrl}/paymentType`);
  }
}
