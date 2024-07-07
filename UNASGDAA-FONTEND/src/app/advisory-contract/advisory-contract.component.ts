import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import { AdvisoryContractService } from '../advisory-contract.service';
import { FilterLineComponent } from '../filter-line/filter-line.component';

@Component({
  selector: 'app-advisory-contract',
  standalone: true,
  imports: [CommonModule, HttpClientModule,
    TranslateModule,FilterLineComponent], 
  templateUrl: './advisory-contract.component.html',
  styleUrls: ['./advisory-contract.component.scss']
})
export class AdvisoryContractComponent implements OnInit {
  advisoryContracts: any[] = [];
  filters={
    line:"ALL",
     
  }

  constructor(private advisoryContractService: AdvisoryContractService) {}

  ngOnInit(): void {
    this.loadContracts();
  }
  loadContracts(): void {
    const params={
      Line: this.filters.line,
    }
    this.advisoryContractService.getAdvisoryContracts().subscribe(
      (response) => {
        console.log('Response:', response);
        this.advisoryContracts = response.data.items;
      },
      (error) => {
        console.error('Error fetching advisory contracts', error);
      }
    );
  }
  onLineChanged(line: string): void {
    this.filters.line = line;
     this.loadContracts();
  }
}
