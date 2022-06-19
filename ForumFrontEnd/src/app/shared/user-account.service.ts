import { Inject, inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

import { USER_ACCOUNT_API_URL } from '../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Token } from '../models/token';

export const ACCES_TOKEN = 'jwt acces token';

@Injectable({
  providedIn: 'root'
})
export class UserAccountService {
  constructor(
    private http: HttpClient,
    @Inject(USER_ACCOUNT_API_URL) private userAccountUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
    ) { }

  login(email: string, password: string): Observable<Token>{
    return this.http.post<Token>(`${this.userAccountUrl}/login`,{
      email, password
    }).pipe(
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
}
