import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserAccountService } from '../shared/user-account.service';

@Injectable({
  providedIn: 'root'
})
export class HasRoleGuard implements CanActivate {

  constructor(private service: UserAccountService, private router: Router){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(!(this.service.user.role.toLowerCase() !== '' &&  route.data['role'].includes(this.service.user.role.toLowerCase()))){
        this.router.navigate(['/']);
      }
    return true;
  }
  
}
