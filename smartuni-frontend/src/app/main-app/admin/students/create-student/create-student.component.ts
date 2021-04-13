import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { GroupsService } from 'src/app/shared/services/groups.service';
import { NotificationsService } from 'src/app/shared/services/notifications.service';
import { StudentsService } from 'src/app/shared/services/students.service';

@Component({
  selector: 'create-student',
  templateUrl: './create-student.component.html',
  styleUrls: ['./create-student.component.scss'],
})
export class CreateStudentComponent implements OnInit {
  createAccountForm = new FormGroup({
    email: new FormControl('mihai@mailinator.com', [Validators.required, Validators.email]),
    firstName: new FormControl('test', [Validators.required]),
    lastName: new FormControl('test', [Validators.required]),
    numericCode: new FormControl('test', [Validators.required]),
    phone: new FormControl('test', [Validators.required]),
    group: new FormControl('', [Validators.required]),
  })
  groups: any[] = [];

  public get email() { return this.createAccountForm.get('email'); }
  public get password() { return this.createAccountForm.get('password'); }
  public get firstName() { return this.createAccountForm.get('firstName'); }
  public get lastName() { return this.createAccountForm.get('lastName'); }
  public get numericCode() { return this.createAccountForm.get('numericCode'); }
  public get phone() { return this.createAccountForm.get('phone'); }
  public get group() { return this.createAccountForm.get('group'); }

  constructor(private groupService: GroupsService,
     private studentService: StudentsService,
     private notificationsService: NotificationsService,
     private dialogRef: MatDialogRef<CreateStudentComponent>
    ) {}
  async ngOnInit() {
    this.groups = await this.groupService.getAllGroups();
    console.log(this.groups);
    
  }

  public async createStudent() {
    console.log(this.createAccountForm);
    const createResult = await this.studentService.createStudentAccount(this.firstName.value, this.lastName.value, this.email.value, this.phone.value, this.numericCode.value);
    console.log(createResult);
    if(createResult) {
      if(this.group.value) {
       await this.groupService.addStudentsToGroup(this.group.value.id, [createResult.id]);
      }
      this.notificationsService.ShowInfo("Student account created")
      this.dialogRef.close({
        ...createResult,
        groupName: this.group.value.name,
        year: this.group.value.year,
      })
    } else {
      this.notificationsService.ShowError("Error creating student account")
    }

 }
}
