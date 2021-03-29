import {
  HttpErrorResponse,
  HttpEvent,
  HttpEventType,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from "@angular/common/http";
import {Observable} from "rxjs";
import {map, tap} from "rxjs/operators";
import {Injectable} from "@angular/core";
import {ServiceResult} from "../models/serviceResult";
import {MessageService} from "primeng/api";

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {

  constructor(private messageService: MessageService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(map((res)=>{

      if(res instanceof HttpResponse){
        if(res.type == HttpEventType.Response) {
          let result: ServiceResult<object> = JSON.parse(JSON.stringify(res.body));

          if(result.errorOccured){
            this.messageService.add({ severity:'error', summary:'შეცდომა', detail: result.message, sticky: true });
          }else if(result.success == false){
            this.messageService.add( {severity:'warn', summary:'', detail: result.message, life: 4000, closable: false});
          }

          const copiedRes = res.clone({ body: result.data});

          return copiedRes;
        }
      }

      return res;

    },(err) => {
      if(err instanceof HttpErrorResponse){
        if(err.status === 500 || err.status == 400) {
          //console.log(err);
        }
      }
    }));
  }
}
