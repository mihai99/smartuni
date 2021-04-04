import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { NotificationsService } from 'src/app/shared/services/notifications.service';

@Component({
  selector: 'student-header',
  templateUrl: './student-header.component.html',
  styleUrls: ['./student-header.component.scss']
})
export class StudentHeaderComponent implements OnInit {

  constructor(
    private authService: AuthService, 
    private router: Router,
    private notificationService: NotificationsService) { }

  ngOnInit(): void {
  }

  public async SignOut() {
    const signOutResult = await this.authService.SignOut();
    this.notificationService.ShowInfo("You logged out");
    this.router.navigate(['/'])
  }
}
