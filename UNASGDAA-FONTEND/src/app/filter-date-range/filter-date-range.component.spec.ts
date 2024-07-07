import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterDateRangeComponent } from './filter-date-range.component';

describe('FilterDateRangeComponent', () => {
  let component: FilterDateRangeComponent;
  let fixture: ComponentFixture<FilterDateRangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FilterDateRangeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FilterDateRangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
