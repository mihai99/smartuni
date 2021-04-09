import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { GroupsService } from 'src/app/shared/services/groups.service';

@Component({
  selector: 'add-group-dialog',
  templateUrl: './add-group-dialog.component.html',
  styleUrls: ['./add-group-dialog.component.scss']
})
export class AddGroupDialogComponent {
  newGroupForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    year: new FormControl('', [Validators.required]),
  })

  public get name() { return this.newGroupForm.get("name") }
  public get year() { return this.newGroupForm.get("year") }

  constructor(
    private groupService: GroupsService,
    private dialogRef: MatDialogRef<AddGroupDialogComponent>) { }

  public async createGroup() {
    console.log(this.year);
    
    const createResponse = await this.groupService.createGroup(this.name.value, this.year.value);
    if(createResponse) {
      this.dialogRef.close(createResponse);
    }
  }
}
