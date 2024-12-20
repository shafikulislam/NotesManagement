import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class NoteService {
    private baseUrl = '/notes';

    constructor(private apiService: ApiService) { }

    // Get all notes by type
    getNotesByType(type: string): Observable<any[]> {
        return this.apiService.get(`${this.baseUrl}/${type}`);
    }

    // Get all notes by id
    getNotesById(type: string, noteId: string): Observable<any[]> {
        return this.apiService.get(`${this.baseUrl}/${type}/${noteId}`);
    }

    // Create a new note
    createNote(type: string, noteData: any): Observable<any> {
        return this.apiService.post(`${this.baseUrl}/${type}`, noteData);
    }

    // Update an existing note
    updateNote(type: string, noteId: string, noteData: any): Observable<any> {
        return this.apiService.put(`${this.baseUrl}/${type}/${noteId}`, noteData);
    }

    // Delete a note by ID
    deleteNote(type: string, noteId: string): Observable<any> {
        return this.apiService.delete(`${this.baseUrl}/${type}/${noteId}`);
    }
}
