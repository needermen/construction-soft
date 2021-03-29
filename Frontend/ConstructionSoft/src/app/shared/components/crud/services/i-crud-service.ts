import {Observable} from "rxjs";
import {ListResult} from "../../../models/listResult";

export interface ICrudService<T> {
  get(page: number, rows: number, search: string) : Observable<ListResult<T>>;
  getById(id: number) : Observable<T>;
  add(object: T) : Observable<number>;
  update(id: number, object: T) : Observable<boolean>;
  delete(id: number) : Observable<boolean>;
}
