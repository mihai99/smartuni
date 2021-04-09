import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { StudentsService } from 'src/app/shared/services/students.service';
import {map, startWith} from 'rxjs/operators';
import { GroupsService } from 'src/app/shared/services/groups.service';

@Component({
  selector: 'add-students-modal',
  templateUrl: './add-students-modal.component.html',
  styleUrls: ['./add-students-modal.component.scss']
})
export class AddStudentsModalComponent implements OnInit {
  avalabileStudents: any[];
  studentsToAdd: any[];

  constructor(
    private dialogRef: MatDialogRef<AddStudentsModalComponent>,
    private groupService: GroupsService,
    private studentService: StudentsService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.getAvailableStudents();
  }

  async getAvailableStudents() {
    this.avalabileStudents = (await this.studentService.getAll()).filter(x => !this.data.studentIds.includes(x.id));
  }

  public get mappedStudents() {
    if(this.avalabileStudents) {
      return this.avalabileStudents.map(student => ({
        value: student,
        description: `${student.firstName} ${student.lastName}`,
        searchField: `${student.firstName} ${student.lastName} ${student.email}`,
      }))
    } 
    return [];
  }

  public setStudentsToAdd(students) {
    this.studentsToAdd = students;
  }

  public async addStudents() {
    const newStudents = await this.groupService.addStudentsToGroup(this.data.groupId, this.studentsToAdd.map(x => x.id));
    this.dialogRef.close(newStudents);
  }

}
