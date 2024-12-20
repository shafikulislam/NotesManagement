import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NoteService } from '../../../../../src/app/core/services/note.service';

@Component({
  selector: 'app-note-form',
  templateUrl: './note-form.component.html',
  styleUrls: ['./note-form.component.css'],
})
export class NoteFormComponent implements OnInit {
  noteForm: FormGroup;
  noteType: string = '';
  noteId: string | null = null;
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private noteService: NoteService
  ) {
    this.noteForm = this.fb.group({
      id: [''],
      //title: ['', [Validators.required]],
      content: [''],
      reminderDate: [''],
      dueDate: [''],
      isComplete: [false],
      url: [''],
    });
  }

  ngOnInit(): void {
    // Fetch query params to determine note type and mode
    this.route.queryParams.subscribe((params) => {
      this.noteType = params['type'] || '';
    });

    // Check for edit mode and fetch note ID if present
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEditMode = true;
        this.noteId = params['id'];
        this.loadNoteDetails();
      }
    });

    this.updateFormFields();
  }

  // Load existing note details in edit mode
  loadNoteDetails(): void {
    if (this.noteId && this.noteType) {
      this.noteService.getNotesById(this.noteType, this.noteId).subscribe((response: any) => {
        const note = response;

        if (note) {

          this.noteForm.patchValue({
            id: note.id,
            content: note.content,
            reminderDate: note.reminderDate ? new Date(note.reminderDate).toISOString().split('T')[0] : null,
            dueDate: note.dueDate ? new Date(note.dueDate).toISOString().split('T')[0] : null,
            isComplete: note.isComplete !== undefined ? note.isComplete : false,
            url: note.url,
          });

          //   this.noteForm.patchValue({
          //     content: note.content,
          //   });
          //   if (this.noteType === 'reminder') {
          //     this.noteForm.patchValue({
          //       reminderDate: note.reminderDate ? new Date(note.reminderDate).toISOString().split('T')[0] : null,
          //     });
          //   } else if (this.noteType === 'todo') {
          //     this.noteForm.patchValue({
          //       dueDate: note.dueDate ? new Date(note.dueDate).toISOString().split('T')[0] : null,
          //       isComplete: note.isComplete !== undefined ? note.isComplete : false,
          //     });
          //   } else if (this.noteType === 'bookmark') {
          //     this.noteForm.patchValue({
          //       url: note.url,
          //     });
          //   }


        }

      });
    }
  }


  // Dynamically update form fields based on note type
  updateFormFields(): void {

    this.noteForm.get('content')?.setValidators([Validators.required]);

    if (this.noteType === 'reminder') {
      this.noteForm.get('reminderDate')?.setValidators([Validators.required]);
    } else if (this.noteType === 'todo') {
      this.noteForm.get('dueDate')?.setValidators([Validators.required]);
      this.noteForm.get('isComplete')?.setValidators([Validators.required]);
    } else if (this.noteType === 'bookmark') {
      this.noteForm.get('content')?.removeValidators([Validators.required]);
      this.noteForm.get('url')?.setValidators([Validators.required, Validators.pattern('https?://.+')]);
    } else {
      // Remove unnecessary validators
      this.noteForm.get('reminderDate')?.clearValidators();
      this.noteForm.get('dueDate')?.clearValidators();
      this.noteForm.get('isComplete')?.clearValidators();
      this.noteForm.get('url')?.clearValidators();
    }

  }

  // Submit the form (create or edit)
  onSubmit(): void {
    if (this.noteForm.valid) {
      const formData = this.noteForm.value;
      if (this.isEditMode && this.noteId !== null) {
        this.noteService.updateNote(this.noteType, this.noteId, formData).subscribe(() => {
          this.router.navigate(['/notes/dashboard']);
        });
      } else {
        this.noteService.createNote(this.noteType, formData).subscribe(() => {
          this.router.navigate(['/notes/dashboard']);
        });
      }
    }
  }


  // Handle Cancel action
  cancelCreate(): void {
    this.router.navigate(['/notes/dashboard']);
  }

}
