import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { GroupsService } from 'src/app/shared/services/groups.service';
import { AddGroupDialogComponent } from './add-group-dialog/add-group-dialog.component';

@Component({
  selector: 'groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  groups = [];
  selectedGroup: any = null;

  constructor(
    private dialog: MatDialog,
    private groupService: GroupsService) { }

  async ngOnInit() {
    const allGroups = await this.groupService.getAllGroups();
    this.groups = allGroups;
    console.log(this.groups);

  }

  public openDialog() {
    const dialogRef = this.dialog.open(AddGroupDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.groups.push(result);
      }
    });
  }

  public async deleteGroup(id) {
    const deleteResult = await this.groupService.deleteGroup(id);
    if (deleteResult) {
      this.groups.splice(this.groups.findIndex(x => x.id === id), 1);
    }
  }

  public updateDataBallback(data) {
    const groupIndex = this.groups.findIndex(x => x.id === data.id);
    if (groupIndex !== -1) {
      this.groups[groupIndex][data.prop] = data.value;
    }
  }
}
