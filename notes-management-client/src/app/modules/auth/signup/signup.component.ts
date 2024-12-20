import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../../../src/app/core/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  signupForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.signupForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit() {
    if (this.signupForm.valid) {
      const { name, email, dateOfBirth, password } = this.signupForm.value;

      this.authService.signup({ name, email, dateOfBirth, password }).subscribe({
        next: (response) => {
          //console.log('signup successful:', response);
          this.router.navigate(['/notes/dashboard']); // Redirect to dashboard after signup
        },
        error: (error) => {
          console.error('signup failed:', error);
          alert('signup failed. Please check your credentials.');
        }
      });
    }
  }
}
