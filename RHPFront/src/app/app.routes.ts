import { Routes } from '@angular/router';
import { authGuard } from './components/auth/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { CreateUserComponent } from './components/user/create-user/create-user.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'home', component: HomeComponent, canActivate: [authGuard] },
    { path: 'new-user', component: CreateUserComponent},
    { path: '**', redirectTo: '/login', pathMatch: 'full' },
];
