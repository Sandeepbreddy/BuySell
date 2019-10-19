import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  loginmodel: any = {};

  // Injecting the Auth Service
  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {}

  // This is called as associated with the Form
  login() {
    this.authService.login(this.loginmodel).subscribe(
      next => {
        this.alertify.success('Login Success');
      },
      error => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/home']);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logOut() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }
}
