import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Token } from '@angular/compiler';
@Injectable({
  providedIn: 'root'
})
export class UserAccountService {
  readonly BaseURL = 'http://localhost:44387/api/useraccounts/';
  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<Token>{

  }

  isAuthenticated(): boolean{

  }

  logout():void {
    
  }
}
