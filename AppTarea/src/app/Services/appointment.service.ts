import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Appointment } from '../Interfaces/appointment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  private apiUrl = 'https://localhost:7001/api/v1/Appointment'; // Ajusta la URL a tu API real

  constructor(private http: HttpClient) { }

  getList(): Observable<Appointment[]> {
    return this.http.get<{ data: { items: Appointment[] } }>(this.apiUrl).pipe(
      map(response => {
        console.log('Response from API:', response); // Debugging log
        if (response && response.data && response.data.items && Array.isArray(response.data.items)) {
          return response.data.items;
        } else {
          throw new Error('La respuesta no contiene un array en la propiedad items');
        }
      })
    );
  }

  add(appointment: Appointment): Observable<Appointment> {
    return this.http.post<Appointment>(this.apiUrl, appointment);
  }

  update(appointment: Appointment): Observable<Appointment> {
    return this.http.put<Appointment>(`${this.apiUrl}/${appointment.id}`, appointment);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
