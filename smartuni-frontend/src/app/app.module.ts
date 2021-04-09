import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { HeaderComponent } from './landing-page/header/header.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { RegisterComponent } from './landing-page/register/register.component';
import {MatInputModule} from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './landing-page/login/login.component';
import { AngularFireModule } from '@angular/fire';
import { environment } from 'src/environments/environment';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AuthService } from './shared/services/auth.service';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { HttpClientModule } from '@angular/common/http';
import { StudentDashboardComponent } from './main-app/student/student-dashboard/student-dashboard.component';
import { StudentHeaderComponent } from './main-app/student/student-header/student-header.component';
import { AdminDashboardComponent } from './main-app/admin/admin-dashboard.component';
import { AdminHeaderComponent } from './main-app/admin/admin-header/admin-header.component';
import { GroupsComponent } from './main-app/admin/groups/groups.component';
import { AddGroupDialogComponent } from './main-app/admin/groups/add-group-dialog/add-group-dialog.component';
import {MatDialogModule} from '@angular/material/dialog';
import { EditGroupComponent } from './main-app/admin/groups/edit-group/edit-group.component';
import { EditStudentsComponent } from './main-app/admin/groups/edit-group/edit-students/edit-students.component';
import { AddStudentsModalComponent } from './main-app/admin/groups/edit-group/edit-students/add-students-modal/add-students-modal.component';
import { MultipleSelectComponent } from './shared/components/multiple-select/multiple-select.component';
import {MatSelectModule} from '@angular/material/select';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    HeaderComponent,
    RegisterComponent,
    LoginComponent,
    StudentDashboardComponent,
    StudentHeaderComponent,
    AdminDashboardComponent,
    AdminHeaderComponent,
    GroupsComponent,
    AddGroupDialogComponent,
    EditGroupComponent,
    EditStudentsComponent,
    AddStudentsModalComponent,
    MultipleSelectComponent
  ],
  imports: [
    AngularFireModule.initializeApp(environment.firebase),
    AngularFireAuthModule,
    FormsModule,
    MatDialogModule,
    BrowserModule,
    AppRoutingModule,
    MatAutocompleteModule,
    MatSelectModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    MatSnackBarModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
