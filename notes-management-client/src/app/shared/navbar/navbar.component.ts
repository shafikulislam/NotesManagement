import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../src/app/core/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(private authService: AuthService, private router: Router) { }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  // Logout the user
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/auth/login']); // Redirect to the login page after logout
  }
}
