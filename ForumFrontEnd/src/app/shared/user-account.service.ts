import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { flatMap, Observable, tap } from 'rxjs';
import { USER_ACCOUNT_API_URL } from '../app-injection';
import { Registration } from '../models/registration.model';
import { Token } from '../models/token';

export const ACCES_TOKEN = 'jwt acces token';

@Injectable({
  providedIn: 'root'
})
export class UserAccountService {

  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
  constructor(
    private http: HttpClient,
    @Inject(USER_ACCOUNT_API_URL) private userAccountUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
    ) { }

  login(email: string, password: string): Observable<Token>{
    return this.http.post<Token>(`${this.userAccountUrl}login`,{
      Email: email, Password: password
    }, this.options).pipe(
      tap(token => localStorage.setItem(ACCES_TOKEN, token.access_token))
    )
  }

  isAuthenticated(): boolean{
    var token = localStorage.getItem(ACCES_TOKEN);
    
    return token !== null && !this.jwtHelper.isTokenExpired(token)
  }

  logout():void {
    localStorage.removeItem(ACCES_TOKEN);
    this.router.navigate(['']);
  }

  register(registerModel: Registration): void{
    this.http.post(`${this.userAccountUrl}register`,{
      registerModel
    }).pipe(flatMap(()=> this.login(registerModel.Email, registerModel.Password)));
  }
}
