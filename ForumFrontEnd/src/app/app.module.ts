import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { JwtModule} from '@auth0/angular-jwt';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { ForumThreadComponent } from './forum-thread/forum-thread.component';
import { PostListComponent } from './forum-thread/post-list/post-list.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { StatisticComponent } from './statistic/statistic.component';
import { ThreadListComponent } from './thread-list/thread-list.component';
import { UserListComponent } from './user-list/user-list.component';
import { RoleListComponent } from './role-list/role-list.component';
import { ThemeListComponent } from './theme-list/theme-list.component';
import { LogoComponent } from './logo/logo.component';
import { FORUM_API_URL } from './app-injection-tokens';
import { environment } from 'src/environments/environment';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    ForumThreadComponent,
    PostListComponent,
    RegistrationComponent,
    LoginComponent,
    StatisticComponent,
    ThreadListComponent,
    UserListComponent,
    RoleListComponent,
    ThemeListComponent,
    LogoComponent,

  ],
  imports: [
    HttpClientModule,
    BrowserModule,

    JwtModule
  ],
  providers: [{
    provide: FORUM_API_URL,
    useValue: environment.forumApiUrl
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
