import { TestBed, inject } from "@angular/core/testing";
import {} from "jasmine";
import { FaceDetectionService } from "./face-detection.service";

describe("FaceDetectionService",
    () => {
        beforeEach(() => {
            TestBed.configureTestingModule({
                providers: [FaceDetectionService]
            });
        });

        it("should be created",
            inject([FaceDetectionService],
                (service: FaceDetectionService) => {
                    expect(service).toBeTruthy();
                }));
    });