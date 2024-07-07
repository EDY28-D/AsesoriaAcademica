import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterLineComponent } from './filter-line.component';

describe('FilterLineComponent', () => {
  let component: FilterLineComponent;
  let fixture: ComponentFixture<FilterLineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FilterLineComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FilterLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
