import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  constructor(private httpService: HttpClient) { }

  public async createStudentAccount( firstName: string, lastName: string, email: string, phoneNumber: string, numericCode: string
  ): Promise<any> {
    const createdStudent = await this.httpService.post(`https://localhost:44393/api/Students`, {
      firstName,
      lastName,
      email,
      phoneNumber,
      numericCode
    }).toPromise();
    return createdStudent;
  }

  public async getStudentsFromGroup(groupId): Promise<any[]> {
    const students = await this.httpService.get<any[]>(`https://localhost:44393/api/Students/in-group/${groupId}`).toPromise();
    return students;
  }

  public async getAll(): Promise<any[]> {
    const allGroups = await this.httpService.get<any[]>("https://localhost:44393/api/Students").toPromise();
    return allGroups;
  }

  public async deleteStudent(studentId: string): Promise<any> {
    const result = await this.httpService.delete(`https://localhost:44393/api/Students/${studentId}`).toPromise();
    return result;
  }
}
