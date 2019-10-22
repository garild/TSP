import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models';

@Injectable({ providedIn: "root" })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem("currentUser"))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  baseUrl = "";

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(userName: string, password: string, teamName: string) {
    return this.http
      .post<User>(`${this.baseUrl}/api/auth/login`, {
        login: userName,
        password: password,
        assignedTeam: teamName
      })
      .pipe(
        map(user => {
          if (user && user.accessToken) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            // in futher its should be asve as Cookie
            localStorage.setItem("currentUser", JSON.stringify(user));
            this.currentUserSubject.next(user);
          }
          return user;
        })
      );
  }

  logout() {
    localStorage.removeItem("currentUser");
    this.currentUserSubject.next(null);
  }
}
