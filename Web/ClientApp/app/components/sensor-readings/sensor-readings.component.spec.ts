import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SensorReadingsComponent } from './sensor-readings.component';

describe('SensorReadingsComponent', () => {
  let component: SensorReadingsComponent;
  let fixture: ComponentFixture<SensorReadingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SensorReadingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SensorReadingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
