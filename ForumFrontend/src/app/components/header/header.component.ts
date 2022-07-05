import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccessService } from 'src/app/shared/access.service';
import { TokenService } from 'src/app/shared/token.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  userId: number=0;

  constructor(
    private tokenService: TokenService,
    private router: Router,
    private accessService: AccessService) { }

  ngOnInit(): void {
  }
    
  isAuthenticated(){
    return this.tokenService.isAuthenticated();
  }

  toProfile(){
    this.userId=this.tokenService.currentUser().id;
    this.router.navigate([`users/${this.userId}`]);
  }

  logout(){
    this.tokenService.logout();
  }

  isAdministratable(){
    return this.accessService.isAdministratable();
  }

  isModeratable(){
    return this.accessService.isModeratable();
  }
}
