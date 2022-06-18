import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
