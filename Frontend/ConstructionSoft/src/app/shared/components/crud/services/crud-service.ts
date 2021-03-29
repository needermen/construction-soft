
import {Observable} from "rxjs";
import {ListResult} from "../../../models/listResult";
import {HttpClient, HttpParams} from "@angular/common/http";
import {ICrudService} from "./i-crud-service";

export class CrudService<T> implements ICrudService<T> {
  constructor(public http: HttpClient, public url: string){

  }

  public add(object: T): Observable<number> {
    return this.http.post<number>(this.url, object);
  }

  public delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.url}/${id}`);
  }

  public get(page: number, rows: number, search: string): Observable<ListResult<T>> {
    return this.http.get<ListResult<T>>(this.url, { params : new HttpParams()
        .set('page', page.toString())
        .set('search', search.toString())
        .set('count',rows.toString())})
  }

  public getById(id: number): Observable<T> {
    return this.http.get<T>(`${this.url}/${id}`);
  }

  public update(id : number, object: T): Observable<boolean> {
    return this.http.put<boolean>(`${this.url}/${id}`, object);
  }

}
