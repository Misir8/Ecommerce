import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {BehaviorSubject, of} from "rxjs";
import {IUser} from "../shared/models/user";
import {map} from "rxjs/operators";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  url = environment.baseUrl;
  private currentUserSource = new BehaviorSubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }


  loadCurrentUser(token: string) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get(this.url + 'account', {headers}).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  login(values: any){
    return this.http.post(this.url + 'account/login', values).pipe(
      map((user: IUser) => {
        this.setUserTolocalStorage(user);
      })
    )
  }

  register(values: any){
    return this.http.post(this.url + 'account/register', values).pipe(
      map((user: IUser) => {
        this.setUserTolocalStorage(user);
      })
    )
  }

  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string){
    return this.http.get(this.url + 'account/emailExists?email=' + email)
  }

  private setUserTolocalStorage(user: IUser){
    if (user){
      localStorage.setItem('token', user.token);
      this.currentUserSource.next(user);
    }
  }
}

