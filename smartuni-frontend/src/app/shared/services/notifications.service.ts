import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private _snackBar: MatSnackBar) { }

  public ShowInfo(message, duration=500) {
    this._snackBar.open(message, 'X', {
      duration: duration,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: 'snackbar-info',
    })
  }
  
  public ShowError(message, duration=500) {
    this._snackBar.open(message, 'X', {
      duration: duration,
      horizontalPosition: 'right',
      verticalPosition: 'top',
      panelClass: 'snackbar-error',
    })
  }
}
