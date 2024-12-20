import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './modules/auth/login/login.component';
import { SignupComponent } from './modules/auth/signup/signup.component';
import { DashboardComponent } from './modules/notes/dashboard/dashboard.component';
import { NoteFormComponent } from './modules/notes/note-form/note-form.component';


const routes: Routes = [
  { path: '', redirectTo: 'notes/dashboard', pathMatch: 'full' }, // Default route
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/signup', component: SignupComponent },
  { path: 'notes/dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'dashboard', redirectTo: 'notes/dashboard' },
  { path: 'notes/create', component: NoteFormComponent, canActivate: [AuthGuard] },
  { path: 'notes/edit/:id', component: NoteFormComponent, canActivate: [AuthGuard] },


  { path: '**', redirectTo: 'notes/dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
