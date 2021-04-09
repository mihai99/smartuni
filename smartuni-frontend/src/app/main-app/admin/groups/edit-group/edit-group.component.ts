import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.scss']
})
export class EditGroupComponent implements OnInit {
  @Input() groupName: string = "";
  @Input() groupYear: string = "";
  @Input() groupId: string = "";
  @Output() closeGroupEditor: EventEmitter<boolean> = new EventEmitter();
  @Output() public updateGroupData: EventEmitter<any> = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  closeEditor() {
    this.closeGroupEditor.emit(true);
  }

}
