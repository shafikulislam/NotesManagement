import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { tap, switchMap } from 'rxjs/operators';


@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private baseUrl = "/auth";

    constructor(private apiService: ApiService) { }

    login(credentials: { email: string; password: string }): Observable<any> {
        return this.apiService.post(`${this.baseUrl}/login`, credentials).pipe(
            tap(response => {
                if (response && response.token) {
                    localStorage.setItem('authToken', response.token);  // Store token in localStorage
                }
            })
        );
    }

    signup(userData: { name: string; email: string; dateOfBirth: Date; password: string }): Observable<any> {
        return this.apiService.post(`${this.baseUrl}/signup`, userData).pipe(
            switchMap(() => {
                // After successful signup, log the user in automatically
                return this.login({ email: userData.email, password: userData.password });
            })
        );
    }

    logout(): void {
        localStorage.removeItem('authToken');
    }

    getToken(): string | null {
        return localStorage.getItem('authToken');
    }

    isAuthenticated(): boolean {
        const token = this.getToken();
        return (!!token);
    }
}
