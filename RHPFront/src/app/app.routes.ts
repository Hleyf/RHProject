import { Routes } from '@angular/router';
import { authGuard } from './components/auth/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { CreateUserComponent } from './components/user/create-user/create-user.component';
import { HallListComponent } from './components/hall/hall-list/hall-list.component';
import { HallComponent } from './components/hall/hall/hall.component';
import { ContactListComponent } from './components/contacts/contact-list/contact-list.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'new-user', component: CreateUserComponent},
    { path: 'home', component: HomeComponent, canActivate: [authGuard] },
    { path: 'halls', component: HallListComponent, canActivate: [authGuard]},
    { path: 'hall/:id', component: HallComponent, canActivate: [authGuard]},
    { path: 'contacts', component: ContactListComponent, canActivate: [authGuard]},
    { path: '**', redirectTo: '/login', pathMatch: 'full' },
];
