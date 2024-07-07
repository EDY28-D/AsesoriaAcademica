export interface Appointment {
  id: string;
  professorId: string;
  studentId: string;
  calendarId: string;
  dateTime: string; // ISO string
  professorProgress: string;
  studentProgress: string;
  status: string;
  googleEventId: string;
  deleted: boolean; // Añadir este campo
}
