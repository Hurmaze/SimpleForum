import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Inject, Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, tap } from 'rxjs';
import { USER_ACCOUNT_API_URL } from '../app-injection';
import { Registration } from '../models/registration.model';
import { Token } from '../models/token';
import { UserToken } from '../models/user-token.model';
import { User } from '../models/user.model';

export const ACCES_TOKEN = 'jwt acces token';

@Injectable({
  providedIn: 'root'
})
export class UserAccountService implements OnInit {

  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };
  user: UserToken = new UserToken(0,'','');
  constructor(
    private http: HttpClient,
    @Inject(USER_ACCOUNT_API_URL) private userAccountUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
    ) { }

  ngOnInit(){
    this.user = this.getUser(ACCES_TOKEN);
  }

  login(email: string, password: string): Observable<Token>{
    return this.http.post<Token>(`${this.userAccountUrl}login`,{
      Email: email, Password: password
    }, this.options).pipe(
      tap(token => {
        localStorage.setItem(ACCES_TOKEN, token.access_token);
        this.user = this.getUser(token.access_token)
      })
    )
  }

  currentUser(): UserToken{
    return this.user;
  }

  getUserRole(): string{
    return this.user.role;
  }

  isAuthenticated(): boolean{
    var token = localStorage.getItem(ACCES_TOKEN);
    
    return token !== null && !this.jwtHelper.isTokenExpired(token)
  }

  logout():void {
    localStorage.removeItem(ACCES_TOKEN);
    this.user = this.getUser(localStorage.getItem(ACCES_TOKEN)!);
    this.router.navigate(['/']);
  }

  register(registerModel: Registration): Observable<User>{

    return this.http.post<User>(`${this.userAccountUrl}`,{
      Email: registerModel.Email,
      Password: registerModel.Password,
      PasswordRepeat: registerModel.PasswordRepeat,
      Nickname: registerModel.Nickname,
    }, this.options);
  }

  getById(id: number): Observable<User>{
    return this.http.get<User>(`${this.userAccountUrl}${id}`, this.options);
  }

  get(): Observable<User[]>{
    return this.http.get<User[]>(`${this.userAccountUrl}`, this.options)
  }

  private getUser(token:string): UserToken{
    if(token === null){
      return new UserToken(0,'','');
    }

    let user = this.jwtHelper.decodeToken(token);
    let userToken = new UserToken(
      Number(user.id), 
      user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"], 
      user['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
    return userToken;
  }
}
