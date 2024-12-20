import { Component, OnInit } from '@angular/core';
import { NoteService } from '../../../../../src/app/core/services/note.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  regularNotes: any[] = [];
  reminderNotes: any[] = [];
  todoNotes: any[] = [];
  bookmarkNotes: any[] = [];

  constructor(private noteService: NoteService, private router: Router) { }

  ngOnInit(): void {
    this.loadNotes();
  }

  loadNotes(): void {
    this.noteService.getNotesByType('regular').subscribe((data) => {
      this.regularNotes = data;
    });
    this.noteService.getNotesByType('reminder').subscribe((data) => {
      this.reminderNotes = data;
    });
    this.noteService.getNotesByType('todo').subscribe((data) => {
      this.todoNotes = data;
    });
    this.noteService.getNotesByType('bookmark').subscribe((data) => {
      this.bookmarkNotes = data;
    });
  }

  // Navigate to the Create Note page based on type
  createNewNote(type: string): void {
    this.router.navigate(['/notes/create'], { queryParams: { type } });
  }

  // Handle Edit Note action
  editNote(type: string, noteId: string): void {
    this.router.navigate(['/notes/edit', noteId], { queryParams: { type } });
  }

  // Handle Delete Note action
  deleteNote(type: string, noteId: string): void {
    this.noteService.deleteNote(type, noteId).subscribe(() => {
      this.loadNotes(); // Reload notes after deletion
    });
  }
}
