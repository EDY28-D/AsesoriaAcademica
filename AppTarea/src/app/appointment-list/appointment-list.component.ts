import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AppointmentFormComponent } from '../appointment-form/appointment-form.component';
import { AppointmentDeleteComponent } from '../appointment-delete/appointment-delete.component';
import { AppointmentService } from '../Services/appointment.service';
import { Appointment } from '../Interfaces/appointment';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css']
})
export class AppointmentListComponent implements OnInit {
  listaAppointments: Appointment[] = [];

  constructor(
    private modalService: NgbModal,
    private appointmentService: AppointmentService
  ) {}

  ngOnInit(): void {
    this.obtenerAppointments();
  }

  obtenerAppointments(): void {
    this.appointmentService.getList().subscribe({
      next: (data: Appointment[]) => {
        this.listaAppointments = data;
      },
      error: (e: any) => {
        console.error('Error al obtener appointments', e);
        this.listaAppointments = []; // Asegurarse de que siempre sea un array
      }
    });
  }

  openFormModal(appointment?: Appointment): void {
    const modalRef = this.modalService.open(AppointmentFormComponent);
    if (appointment) {
      modalRef.componentInstance.appointment = appointment;
    }
    modalRef.result.then((result) => {
      if (result === 'created' || result === 'updated') {
        this.obtenerAppointments();
      }
    }).catch((res) => {});
  }

  openDeleteModal(appointment: Appointment): void {
    const modalRef = this.modalService.open(AppointmentDeleteComponent);
    modalRef.componentInstance.appointmentId = appointment.id;
    modalRef.result.then((result) => {
      if (result === 'deleted') {
        this.obtenerAppointments();
      }
    }).catch((res) => {});
  }
}
