import { TestBed, inject } from '@angular/core/testing';

import { NeuralNetworksService } from './neural-networks.service';

describe('NeuralNetworksService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NeuralNetworksService]
    });
  });

  it('should be created', inject([NeuralNetworksService], (service: NeuralNetworksService) => {
    expect(service).toBeTruthy();
  }));
});
