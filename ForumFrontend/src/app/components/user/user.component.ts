import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from 'src/app/models/role.model';
import { User } from 'src/app/models/user.model';
import { AccessService } from 'src/app/shared/access.service';
import { TokenService } from 'src/app/shared/token.service';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  user: User=new User(0,'','',[],[],'');
  roles: Role[]=[];
  role: string='';
  curRoleId: number=1;

  newNickname: string=''; 
  errorMessage: string='';

  isLoaded: boolean = false;
  isHiddenRole: boolean = true;
  isHiddenNickname: boolean = true;

  constructor(
    private userService: UserService,
    private tokenService: TokenService,
    private accessService: AccessService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location) { }

  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.role = this.tokenService.getUserRole();
    if(this.accessService.isAdministratable())
    {
      this.userService.getRoles().subscribe(res => this.roles = res);
    }

      this.loadPage(routeParams['id'])
    });
  }

  isCurrentUser(){
    return this.user.email === this.tokenService.currentUser().email;
  }

  toggleRole(){
    this.isHiddenRole = !this.isHiddenRole;
  }

  toggleNickname(){
    this.isHiddenNickname = !this.isHiddenNickname;
  }

  changeRole(userId: number, roleId: number){
    this.userService.changeRole(userId, roleId).subscribe(res => window.location.reload());
  }

  changeNickname(nickname:string){
    this.userService.changeNickname(nickname)
    .subscribe(res => window.location.reload(),
    err => this.errorMessage = err.error.ErrorMessage);
  }

  deleteUser(id: number){
    this.userService.deleteUser(id).subscribe(res => {this.location.back()})
  }

  isAdministratable(){
    return this.accessService.isAdministratable();
  }

  private loadPage(id: number){
    this.userService.getById(id)
    .subscribe(res => 
      {
        this.user = res;
        this.curRoleId = res.roleId;
        this.isLoaded = true;
    },
      err => {this.router.navigate(['/'])});
  }

}
