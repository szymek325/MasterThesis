import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import {} from 'jasmine'

import { FaceDetectionsComponent } from './face-detections.component';

describe('FaceDetectionsComponent', () => {
  let component: FaceDetectionsComponent;
  let fixture: ComponentFixture<FaceDetectionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FaceDetectionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FaceDetectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
