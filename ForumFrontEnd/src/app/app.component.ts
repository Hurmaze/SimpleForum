import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { UserAccountService } from './shared/user-account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'The best Forum';

  constructor(private userAccountService: UserAccountService){

  }

  public get isLoggedIn() : boolean {
    return this.userAccountService.isAuthenticated();
  }

  logout(){
    this.userAccountService.logout();
  }

  login(email: string, password: string){
    this.userAccountService.login(email, password)
    .subscribe(result => {
      
    }, (err: HttpErrorResponse) => console.log(err.error));
  }
}
