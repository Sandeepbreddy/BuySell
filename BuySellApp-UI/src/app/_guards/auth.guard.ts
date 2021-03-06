import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    } else {
      this.alertify.error('Please Login');
      this.router.navigate(['/home']);
      return false;
    }
  }
}
