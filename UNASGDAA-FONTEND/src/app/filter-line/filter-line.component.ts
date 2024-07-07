import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-filter-line',
  standalone: true,
  templateUrl: './filter-line.component.html',
  styleUrls: ['./filter-line.component.scss'],
  imports: [CommonModule, FormsModule,TranslateModule]
})
export class FilterLineComponent implements OnInit {
  lines: any[] = [];
  selectedLine: string = 'RESEGTY';

  @Output() lineChanged = new EventEmitter<string>();

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // Reemplaza '/api/lines' con la URL real de tu backend
    this.http.get<any[]>('/api/lines').subscribe(data => {
      this.lines = data;
    });
  }

  onLineChange(): void {
    this.lineChanged.emit(this.selectedLine);
  }
}
