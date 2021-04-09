import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  constructor(private httpService: HttpClient) { }

  public async getStudentsFromGroup(groupId): Promise<any[]> {
    const students = await this.httpService.get<any[]>(`https://localhost:44393/api/Students/in-group/${groupId}`).toPromise();
    return students;
  }

  public async getAll(): Promise<any[]> {
    const allGroups = await this.httpService.get<any[]>("https://localhost:44393/api/Students").toPromise();
    return allGroups;
  }
}
