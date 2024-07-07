import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AppointmentFormComponent } from './appointment-form/appointment-form.component';
import { AppointmentDeleteComponent } from './appointment-delete/appointment-delete.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private modalService: NgbModal) {}

  openFormModal(appointment?: any): void {
    const modalRef = this.modalService.open(AppointmentFormComponent);
    modalRef.componentInstance.appointment = appointment;
    modalRef.result.then((result) => {
      if (result === 'created' || result === 'updated') {
        // Refresh the list or take other actions if needed
      }
    }).catch((res) => {});
  }

  openDeleteModal(appointment: any): void {
    const modalRef = this.modalService.open(AppointmentDeleteComponent);
    modalRef.componentInstance.appointmentId = appointment.id;
    modalRef.result.then((result) => {
      if (result === 'deleted') {
        // Refresh the list or take other actions if needed
      }
    }).catch((res) => {});
  }
}
