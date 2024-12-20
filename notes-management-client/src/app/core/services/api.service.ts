import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from '../../../../src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private router: Router) { }

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken'); // Assuming the token is stored in localStorage
    if (!token) {
      this.router.navigate(['/auth/login']); // Redirect to login if token is unavailable
      throw new Error('No authentication token found');
    }

    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }

  get<T>(endpoint: string): Observable<T> {
    const headers = this.getHeaders();
    return this.http.get<T>(`${this.apiUrl}${endpoint}`, { headers });
  }

  post<T>(endpoint: string, data: any): Observable<T> {
    if (endpoint.startsWith('/auth')) {
      return this.http.post<T>(`${this.apiUrl}${endpoint}`, data);
    } else {
      const headers = this.getHeaders();
      return this.http.post<T>(`${this.apiUrl}${endpoint}`, data, { headers });
    }
  }

  put<T>(endpoint: string, data: any): Observable<T> {
    const headers = this.getHeaders();
    return this.http.put<T>(`${this.apiUrl}${endpoint}`, data, { headers });
  }

  delete<T>(endpoint: string): Observable<T> {
    const headers = this.getHeaders();
    return this.http.delete<T>(`${this.apiUrl}${endpoint}`, { headers });
  }
}
