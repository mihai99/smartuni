import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GroupsService } from 'src/app/shared/services/groups.service';
import { StudentsService } from 'src/app/shared/services/students.service';
import { AddStudentsModalComponent } from './add-students-modal/add-students-modal.component';

@Component({
  selector: 'edit-students',
  templateUrl: './edit-students.component.html',
  styleUrls: ['./edit-students.component.scss']
})
export class EditStudentsComponent {
  private _groupId: string = ""
  students: any[] = [];
  @Input() set groupId(id: string) {
      this._groupId = id;
      this.getStudents();
    }
  @Output() updateGroupData: EventEmitter<any> = new EventEmitter();
  
  constructor(
    private studentsService: StudentsService,
    private groupService: GroupsService,
    private dialog: MatDialog) { }

  async getStudents() {
    this.students = await this.studentsService.getStudentsFromGroup(this._groupId);
    console.log(this.students);
  }

  public async removeStudent(studentId) {
    this.students = await this.groupService.removeStudentsFromGroup(this._groupId, [studentId]);
    this.updateGroupDataCall();
  }

  public openDialog() {
    const dialogRef = this.dialog.open(AddStudentsModalComponent, {
      width: "600px",
      data: {
        groupId: this._groupId,
        studentIds: this.students.map(x => x.id)}
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.students = result;
        this.updateGroupDataCall();
      }
    });
  }

  private updateGroupDataCall() {
    this.updateGroupData.emit({
      id: this._groupId,
      prop: "stundentNumber",
      value: this.students.length
    })
  }
}
