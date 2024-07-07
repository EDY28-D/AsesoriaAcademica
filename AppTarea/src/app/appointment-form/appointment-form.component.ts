import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Appointment } from '../Interfaces/appointment';
import { AppointmentService } from '../Services/appointment.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.css']
})
export class AppointmentFormComponent implements OnInit {

  @Input() appointment: Appointment | null = null;
  formularioAppointment: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _AppointmentServicio: AppointmentService,
    public activeModal: NgbActiveModal
  ) {
    this.formularioAppointment = this.fb.group({
      professorId: ['', Validators.required],
      studentId: ['', Validators.required],
      calendarId: ['', Validators.required],
      dateTime: ['', Validators.required],
      professorProgress: ['', Validators.required],
      studentProgress: ['', Validators.required],
      status: ['', Validators.required],
      googleEventId: ['', Validators.required],
      deleted: [false]
    });
  }

  ngOnInit(): void {
    if (this.appointment) {
      this.formularioAppointment.patchValue(this.appointment);
    }
  }

  guardarAppointment(): void {
    if (this.formularioAppointment.invalid) {
      return;
    }

    const request: Appointment = {
      ...this.formularioAppointment.value,
      id: this.appointment ? this.appointment.id : 0
    };

    if (this.appointment) {
      this._AppointmentServicio.update(request).subscribe({
        next: () => {
          this.activeModal.close('updated');
        },
        error: (e: any) => {
          console.error('Error al actualizar appointment', e);
        }
      });
    } else {
      this._AppointmentServicio.add(request).subscribe({
        next: () => {
          this.activeModal.close('created');
        },
        error: (e: any) => {
          console.error('Error al agregar appointment', e);
        }
      });
    }
  }
}
