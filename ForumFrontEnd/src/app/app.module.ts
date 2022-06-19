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
import { BASE_API_URL, FORUM_THREADS_API_URL, POSTS_API_URL, STATISTICS_API_URL, USER_ACCOUNT_API_URL } from './app-injection-tokens';
import { environment } from 'src/environments/environment';
import { ACCES_TOKEN } from './shared/user-account.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

export function tokenGetter() {
  return localStorage.getItem(ACCES_TOKEN);
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
    FormsModule,
    RouterModule.forRoot(
      [
        { path: "", component: LoginComponent}
      ]
    ),

    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: environment.whiteListedDomains
      }
    })
  ],
  providers: [{
    provide: BASE_API_URL,
    useValue: environment.baseApiUrl
  },
  {
    provide: POSTS_API_URL,
    useValue: environment.postsUrl
  },
  {
    provide: FORUM_THREADS_API_URL,
    useValue: environment.forumThreadsUrl
  },
  {
    provide: STATISTICS_API_URL,
    useValue: environment.statisticsUrl
  },
  {
    provide: USER_ACCOUNT_API_URL,
    useValue: environment.userAccountsUrl
  }
],
  bootstrap: [AppComponent]
})
export class AppModule { }
