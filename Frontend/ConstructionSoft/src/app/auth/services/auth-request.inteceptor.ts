import {AuthService} from "./auth.service";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthRequestInteceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const copiedReq = req.clone({headers: req.headers
        .set('Authorization', this.authService.getToken())
        .set('UserId', this.authService.getUserId())
        .set('OrganizationId', this.authService.getOrganizationId())
    });

    return next.handle(copiedReq);
  }
}
