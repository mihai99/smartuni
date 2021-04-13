import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { StudentsService } from 'src/app/shared/services/students.service';
import { CreateStudentComponent } from './create-student/create-student.component';

export interface StudentData {
  id: string,
  name: string,
  year: string,
  groupName: string,
  phone: string,
  numericCode: string,
}

@Component({
  selector: 'students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.scss'],
})
export class StudentsComponent implements OnInit {

  displayedColumns: string[] = ['numericCode', 'name', 'year', 'groupName', 'phone', 'action', 'select'];
  dataSource: MatTableDataSource<StudentData>;
  selection = new SelectionModel<StudentData>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private studentService: StudentsService,
    private dialog: MatDialog) { }

  async ngOnInit() {
    const allStudents = await this.studentService.getAll();
    this.dataSource = new MatTableDataSource(allStudents.map(x => mapStudent(x)));
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  isAllSelected() {
    if (this.dataSource) {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSource.data.length;
      return numSelected === numRows;
    }
    return false;
  }

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  openCreateStudentDialog() {
    const dialog = this.dialog.open(CreateStudentComponent, {
      width: "400px"
    })
    dialog.afterClosed().subscribe(response => {
      this.dataSource = new MatTableDataSource([response, ...this.dataSource.data]);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    })
  }
  async deleteStudents() {
    await this.selection.selected.map(x => x.id).reduce((previousValue, currentValue) => 
      previousValue.then(_ => this.studentService.deleteStudent(currentValue))
      , Promise.resolve(true))
    this.dataSource = new MatTableDataSource(this.dataSource.data.filter(x => this.selection.selected.findIndex(s => s.id === x.id) === -1));
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  deleteSingleStudent(id) {
    const deleteResult = this.studentService.deleteStudent(id);
    if (deleteResult) {
      this.dataSource = new MatTableDataSource(this.dataSource.data.filter(x => x.id !== id));
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }
}

const mapStudent = (studentData): StudentData => {
  return {
    groupName: studentData.groupName,
    id: studentData.id,
    name: `${studentData.firstName} ${studentData.lastName}`,
    numericCode: studentData.numericCode,
    phone: studentData.phoneNumber,
    year: studentData.year.toString(),
  }
}