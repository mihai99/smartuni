import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotificationsService } from './notifications.service';

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  constructor(
    private httpService: HttpClient,
    private notificationsService: NotificationsService
  ) { }

  public async getAllGroups(): Promise<any[]> {
    const allGroups = await this.httpService.get<any[]>("https://localhost:44393/api/Group", {
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("jwtToken")}`
      }
    }).toPromise();
    return allGroups;
  }

  public async createGroup(name, year) {
    try {
      const createResult = await this.httpService.post("https://localhost:44393/api/Group", {
        name: name,
        year: new Number(year)
      }).toPromise();
      this.notificationsService.ShowInfo("Group created")
      return createResult;
    } catch (error) {
      this.notificationsService.ShowError("Error creating group")
      return false
    }
  }

  public async deleteGroup(id) {
    try {
      await this.httpService.delete(`https://localhost:44393/api/Group/${id}`).toPromise();
      return true;
    } catch {
      return false;
    }
  }

  public async removeStudentsFromGroup(groupId: string, studentIds: string[]) {
      return await this.httpService.put<any[]>(`https://localhost:44393/api/Group/${groupId}/remove-students`, {
        studentIds
      }).toPromise();
  }

  public async addStudentsToGroup(groupId: string, studentIds: string[]) {
    return await this.httpService.put<any[]>(`https://localhost:44393/api/Group/${groupId}/add-students`, {
      studentIds
    }).toPromise();
}
}
