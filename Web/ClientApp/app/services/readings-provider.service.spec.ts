import { TestBed, inject } from '@angular/core/testing';

import { ReadingsProviderService } from './readings-provider.service';

describe('ReadingsProviderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReadingsProviderService]
    });
  });

  it('should be created', inject([ReadingsProviderService], (service: ReadingsProviderService) => {
    expect(service).toBeTruthy();
  }));
});
