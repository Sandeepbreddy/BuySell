import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  loginmodel: any = {};

  // Injecting the Auth Service
  constructor(private authService: AuthService) {}

  ngOnInit() {}

  // This is called as associated with the Form
  login() {
    this.authService.login(this.loginmodel).subscribe(
      next => {
        console.log('Login Success');
      },
      error => {
        console.log(error);
      }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');

    // !! Return true or false value
    return !!token;
  }

  logOut() {
    localStorage.removeItem('token');
    console.log('logged out');
  }
}
