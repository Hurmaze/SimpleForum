import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from 'src/app/models/role.model';
import { User } from 'src/app/models/user.model';
import { UserAccountService } from 'src/app/shared/user-account.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  user: User=new User(0,'','',[],[],'');
  roles: Role[]=[];
  newRoleId: number=0;

  newNickname: string=''; 
  errorMessage: string='';

  isHiddenRole: boolean = true;
  isHiddenNickname: boolean = true;
  allowedToChange: boolean = false;

  constructor(
    private authService: UserAccountService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location) { }

  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.loadPage(routeParams['id'])
    });
    let role = this.authService.getUserRole();
    
    role = role.toLowerCase()
    if("admin" === role)
    {
      this.allowedToChange = true;
      this.authService.getRoles().subscribe(res => this.roles = res);
    }
  }

  isCurrentUser(){
    return this.user.email === this.authService.currentUser().email;
  }

  toggleRole(){
    this.isHiddenRole = !this.isHiddenRole;
  }

  toggleNickname(){
    this.isHiddenNickname = !this.isHiddenNickname;
  }

  changeRole(userId: number, email: string, roleId: number){
    this.authService.changeRole(userId, email, roleId).subscribe(res => window.location.reload());
  }

  changeNickname(nickname:string){
    this.authService.changeNickname(nickname)
    .subscribe(res => window.location.reload(),
    err => this.errorMessage = err.error.ErrorMessage);
  }

  deleteUser(id: number){
    this.authService.deleteUser(id).subscribe(res => {this.location.back()})
  }

  
  isAllowedToChange(): boolean{
    return this.allowedToChange;
  }

  private loadPage(id: number){
    this.authService.getById(id)
    .subscribe(res => {this.user = res},
      err => {this.router.navigate(['/'])});
  }

}
