import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'multiple-select',
  templateUrl: './multiple-select.component.html',
  styleUrls: ['./multiple-select.component.scss']
})
export class MultipleSelectComponent implements OnInit {
  searchControl = new FormControl();
  @Input() options: any[] = [{value: {}, description: "One", searchField: ""}];
  @Output() newValues: EventEmitter<any[]> = new EventEmitter();
  filteredOptions: string[];
  selectedOptions: any[] = [];

  ngOnInit() {
    this.searchControl.valueChanges.subscribe(newValue => {
      this.filteredOptions =  this.options.filter(option => 
        option.searchField.toLowerCase().indexOf(newValue) !== -1 && 
        this.selectedOptions.filter(x => x.description === option.description).length === 0
        );
    });
  }
  
  public selectOption(option) {
    this.searchControl.setValue(null);
    this.selectedOptions.push(option);
    this.newValues.emit(this.selectedOptions.map(x => x.value));
  }
}
