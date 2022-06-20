import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserAccountService } from '../shared/user-account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: UserAccountService, private router: Router){}

  canActivate():boolean {
    if(!this.accountService.isAuthenticated()){
      this.router.navigate(['']);
    }

    return true;
  }
  
}
