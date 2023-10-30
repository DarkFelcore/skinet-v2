import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject, map, of } from 'rxjs';
import { User } from '../shared/models/common/user';
import { environment } from 'src/environments/environment';
import { LoginRequest } from '../shared/models/login-request';
import { RegisterRequest } from '../shared/models/register-request';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
  ) { }

  loadCurrentUser(token: string) {

    if(token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    
    return this.http.get<User>(environment.apiUrl + 'auth', { headers }).pipe(
      map((user: User | null) => {
        if(user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
        return user;
      })
    )
  }

  login(values: LoginRequest) {
    return this.http.post<User>(environment.apiUrl + 'auth/login', values).pipe(
      map((user: User | null) => {
        if(user) localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  register(values: RegisterRequest) {
    return this.http.post<User>(environment.apiUrl + 'auth/register', values).pipe(
      map((user: User | null) => {
        if(user) localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  logout() : void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(environment.apiUrl + 'auth/emailexists?email=' + email)
  }

}
