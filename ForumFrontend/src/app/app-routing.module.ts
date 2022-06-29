import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import { ForumThreadComponent } from './components/forum-thread/forum-thread.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { PostUpdateComponent } from './components/post-update/post-update.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { RoleListComponent } from './components/role-list/role-list.component';
import { ThemeListComponent } from './components/theme-list/theme-list.component';
import { ThreadUpdateComponent } from './components/thread-update/thread-update.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserComponent } from './components/user/user.component';
import { HasRoleGuard } from './guards/has-role.guard';

const routes: Routes =[
    {
        path: '', component: HomeComponent
    },
    {
        path: 'threads', component: HomeComponent
    },
    {
        path: 'login', component: LoginComponent
    },
    {
        path: 'registration', component: RegistrationComponent
    },
    {
        path: 'threads/:id', component: ForumThreadComponent
    },
    {
        path: 'users/:id', component: UserComponent
    },
    {
        path: 'roles', component: RoleListComponent, 
        canActivate: [HasRoleGuard],
        data:{
            role: 'admin'
        }
    },
    {
        path: 'users', component: UserListComponent, 
        canActivate: [HasRoleGuard],
        data:{
            role: 'admin'
        }
    },
    {
        path: 'themes', component: ThemeListComponent,
        canActivate: [HasRoleGuard],
        data:{
            role: ['admin', 'moderator']
        }
    },
    {
        path: 'threads/edit/:id', component: ThreadUpdateComponent,
        canActivate: [HasRoleGuard],
        data:{
            role: ['admin', 'moderator']
        }
    },
    {
        path: 'posts/edit/:id', component: PostUpdateComponent,
        canActivate: [HasRoleGuard],
        data:{
            role: ['admin', 'moderator']
        }
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{}