import {Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Observable} from "rxjs";
import {UserModel} from "./models/userModel";
import {ListResult} from "../../shared/models/listResult";
import {RoleEnum} from "./models/role-enum";

@Injectable()
export class AuthService {
  token: string;

  constructor(private router: Router, private httpclient: HttpClient) {
  }

  login(username: string, password: string): Observable<UserModel> {
    return this.httpclient.post<UserModel>(`${environment.apiUrl}/auth/login`, {
      username: username,
      password: password
    });
  }

  logout(): Observable<boolean> {
    return this.httpclient.post<boolean>(`${environment.apiUrl}/auth/logout`, {
      token: this.token
    });
  }

  changePassword(oldPassword, newPassword) : Observable<boolean> {
    return this.httpclient.put<boolean>(`${environment.apiUrl}/auth/changePassword`, {
      token: this.getToken(),
      oldPassword: oldPassword,
      newPassword: newPassword
    });
  }

  getRoles(): Observable<ListResult<number>> {
    return this.httpclient.post<ListResult<number>>(`${environment.apiUrl}/auth/roles`, {
      token: this.token
    });
  }

  isAuthenticated() {
    const user = this.getUser();

    return user != null && user.token != null && user.tokenExpireDate != null && new Date(user.tokenExpireDate) > new Date();
  }

  updateTokenExpireDate(minutes) {
    var user = this.getUser();
    if (user != null) {
      var date = new Date();
      date.setMinutes(date.getMinutes() + minutes);

      user.tokenExpireDate = date.toString();
      this.setUser(user);
    }
  }

  clearUser() {
    sessionStorage.removeItem('user');
  }

  setUser(user: UserModel) {
    sessionStorage.setItem('user', JSON.stringify(user));
  }

  getUser(): UserModel {
    let user: UserModel = JSON.parse(sessionStorage.getItem('user'));
    return user;
  }

  getToken(): string {
    const user = this.getUser();
    if (user != null) {
      return user.token
    }
    return '';
  }

  getUserId(): string {
    let user: UserModel = JSON.parse(sessionStorage.getItem('user'));
    if (user != null && user.id != null) {
      return user.id.toString();
    }
    return '';
  }

  getOrganizationId(): string {
    let user: UserModel = JSON.parse(sessionStorage.getItem('user'));
    if (user != null && user.organizationId != null) {
      return user.organizationId.toString();
    }
    return '';
  }

  hasRole(role: RoleEnum): boolean {
    let user: UserModel = JSON.parse(sessionStorage.getItem('user'));
    if (user == null) {
      return false;
    }

    return user.roleIds.filter(function (element) {
      return element == role;
    }).length > 0;
  }
}
