import { Routes } from '@angular/router';
import { authGuard } from './components/auth/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { CreateUserComponent } from './components/user/create-user/create-user.component';
import { HallListComponent } from './components/hall/hall-list/hall-list.component';
import { HallComponent } from './components/hall/hall/hall.component';
import { ContacsSidebarComponent } from './shared/components/contacts/contacts-sidebar/contacts-sidebar.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
    { path: '', component: AppComponent, canActivate: [authGuard] },
    { path: 'home', component: HomeComponent, canActivate: [authGuard],
        loadChildren: () => import('./components/home/home.module').then(m => m.HomeModule)
    },
    { path: 'login', component: LoginComponent },
    { path: 'new-user', component: CreateUserComponent},
    { path: 'halls', component: HallListComponent, canActivate: [authGuard]},
    { path: 'hall/:id', component: HallComponent, canActivate: [authGuard]},
    { path: 'contacts', component: ContacsSidebarComponent, canActivate: [authGuard]},
    { path: '**', redirectTo: '/home', pathMatch: 'full' },
];
