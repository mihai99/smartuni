import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { NotificationsService } from './notifications.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userData: any;

  constructor(
    public afAuth: AngularFireAuth,
    private httpService: HttpClient,
    private notificationsService: NotificationsService
  ) {
    this.afAuth.authState.subscribe(user => {
      if (user) {
        this.getJwtToken();
      }
    })
  }

  private async getJwtToken() {
    const currentUser = await this.afAuth.currentUser;
    const jwtToken = await currentUser.getIdToken();
    localStorage.setItem("jwtToken", jwtToken);
    setTimeout(() => this.getJwtToken(), 30*60*1000);
  }

  public async SingIn(email, password) {
    try {
      const loginResult = await this.afAuth.signInWithEmailAndPassword(email, password);
      return loginResult;
    }
    catch (e) {
      console.log(e);
      this.notificationsService.ShowError(e.message, 3000);
      return Promise.resolve(false);
    }
  }

  public async SignUp(email, password, firstName, lastName, phoneNumber, numericCode) {
    const firebaseCreateAccount = await this.afAuth.createUserWithEmailAndPassword(email, password);
    if (!firebaseCreateAccount) {
      return Promise.reject(false);
    }
    const createAccountDetails = await this.httpService.post("https://localhost:44393/api/Students",
      {
        "id": firebaseCreateAccount.user.uid,
        "firstName": firstName,
        "lastName": lastName,
        "email": email,
        "phoneNumber": phoneNumber,
        "numericCode": numericCode
      }
    ).toPromise();
    if (!createAccountDetails) {
      return Promise.reject(false);
    }
    return Promise.resolve(createAccountDetails);
  }

  public async SignOut() {
    return await this.afAuth.signOut().then(console.log);
  }
  private SendVerificationEmail() {
    return this.afAuth.currentUser.then(async currentUser => {
      if (currentUser) {
        await currentUser.sendEmailVerification();
        return true;
      } else {
        return false;
      }
    })
  }
}
