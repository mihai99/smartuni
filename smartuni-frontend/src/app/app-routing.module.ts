import { NgModule } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LoginComponent } from './landing-page/login/login.component';
import { RegisterComponent } from './landing-page/register/register.component';
import { StudentDashboardComponent } from './main-app/student/student-dashboard/student-dashboard.component';
import { AngularFireAuthGuard, redirectLoggedInTo } from "@angular/fire/auth-guard";
import { AdminDashboardComponent } from './main-app/admin/admin-dashboard.component';
import { GroupsComponent } from './main-app/admin/groups/groups.component';

const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '', redirectTo: '/login', pathMatch: 'full' }
    ],
    canActivate: [AngularFireAuthGuard],
    data: {
      authGuardPipe: () => redirectLoggedInTo(['student']),
    }
  },
  {
    path: 'student',
    component: StudentDashboardComponent,
    canActivate: [AngularFireAuthGuard]
  },
  {
    path: 'admin',
    component: AdminDashboardComponent,
    canActivate: [AngularFireAuthGuard],
    children: [
      {path: 'groups', component: GroupsComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
