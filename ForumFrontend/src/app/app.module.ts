import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { ForumThreadComponent } from './components/forum-thread/forum-thread.component';
import { PostListComponent } from './components/forum-thread/post-list/post-list.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { RoleListComponent } from './components/role-list/role-list.component';
import { StatisticComponent } from './components/statistic/statistic.component';
import { LogoComponent } from './components/logo/logo.component';
import { ThreadListComponent } from './components/thread-list/thread-list.component';
import { ThemeListComponent } from './components/theme-list/theme-list.component';
import { BASE_API_URL, FORUM_THREADS_API_URL, POSTS_API_URL, STATISTICS_API_URL, USER_ACCOUNT_API_URL } from './app-injection';
import { environment } from 'src/environments/environment';
import { ACCES_TOKEN } from './shared/user-account.service';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './components/header/header.component';
import { AppRoutingModule } from './app-routing.module';
import { UserComponent } from './components/user/user.component';
import { ThreadUpdateComponent } from './components/thread-update/thread-update.component';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { PostUpdateComponent } from './components/post-update/post-update.component';

export function getToken() {
  return localStorage.getItem(ACCES_TOKEN);
}
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    ForumThreadComponent,
    PostListComponent,
    RegistrationComponent,
    UserListComponent,
    RoleListComponent,
    StatisticComponent,
    LogoComponent,
    ThreadListComponent,
    ThemeListComponent,
    HeaderComponent,
    UserComponent,
    ThreadUpdateComponent,
    PostUpdateComponent,
  ],

  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    ConfirmationPopoverModule.forRoot({
      confirmButtonType: 'danger' 
    }),

    JwtModule.forRoot({
      config:{
        tokenGetter: getToken,
        allowedDomains:["localhost:7265"]
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
