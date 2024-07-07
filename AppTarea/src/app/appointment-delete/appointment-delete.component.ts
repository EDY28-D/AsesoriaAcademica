import { Component, Input } from '@angular/core';
import { AppointmentService } from '../Services/appointment.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-appointment-delete',
  templateUrl: './appointment-delete.component.html',
  styleUrls: ['./appointment-delete.component.css']
})
export class AppointmentDeleteComponent {

  @Input() appointmentId!: string;

  constructor(
    private _AppointmentServicio: AppointmentService,
    public activeModal: NgbActiveModal
  ) {}

  eliminarAppointment(): void {
    this._AppointmentServicio.delete(this.appointmentId).subscribe({
      next: () => {
        this.activeModal.close('deleted');
      },
      error: (e: any) => {
        console.error('Error al eliminar appointment', e);
      }
    });
  }
}
