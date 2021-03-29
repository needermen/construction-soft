import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs";
import {map, tap} from "rxjs/operators";
import {Router} from "@angular/router";
import {Injectable} from "@angular/core";
import {AuthService} from "./auth.service";
import {environment} from "../../../environments/environment";

@Injectable()
export class AuthResponseInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(tap((res)=>{

      this.authService.updateTokenExpireDate(environment.authInterval);

    },(err) => {
      if(err instanceof HttpErrorResponse){
        if(err.status === 401){
          this.authService.clearUser();
          this.router.navigateByUrl('/');
        }
      }
    }));
  }
}
