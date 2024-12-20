import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NotesRoutingModule } from './notes-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NoteFormComponent } from './note-form/note-form.component';



@NgModule({
  declarations: [
    DashboardComponent,
    NoteFormComponent,
  ],
  imports: [
    CommonModule,
    NotesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class NotesModule { }
