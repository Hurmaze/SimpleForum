import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { TokenService } from './shared/token.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ForumFrontend';

  constructor(private tokenService: TokenService){

  }

  public get isLoggedIn() : boolean {
    return this.tokenService.isAuthenticated();
  }

  logout(){
    this.tokenService.logout();
  }

  login(email: string, password: string){
    this.tokenService.login(email, password)
    .subscribe(result => {
      
    }, (err: HttpErrorResponse) => console.log(err.error));
  }
}
