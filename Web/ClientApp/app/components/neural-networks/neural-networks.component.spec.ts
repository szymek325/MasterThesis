import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NeuralNetworksComponent } from './neural-networks.component';

describe('NeuralNetworksComponent', () => {
  let component: NeuralNetworksComponent;
  let fixture: ComponentFixture<NeuralNetworksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NeuralNetworksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NeuralNetworksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
