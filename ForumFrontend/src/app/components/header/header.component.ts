import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authService: UserAccountService, private router: Router) { }

  isAuthenticated(){
    return this.authService.isAuthenticated();
  }

  toLogin(){
    this.router.navigate(['login']);
  }

  logout(){
    this.authService.logout();
  }

  toRegister(){
    this.router.navigate(['registration']);
  }

  isAdmin(){
    let role = this.authService.getUserRole();
    return "admin" === role.toLowerCase();
  }

  isModerator(){
    let role = this.authService.getUserRole();
    return "moderator" === role.toLowerCase();
  }

  ngOnInit(): void {
    
  }

}
