import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { FaceDetectionService } from "../../../services/face-detection.service";
import { IFaceDetectionRequest } from "../../../interfaces/detection/face-detection-request";
import { IDetectionResult } from "../../../interfaces/detection/detection-result";

@Component({
    selector: "face-detection-request",
    templateUrl: "./face-detection-request.component.html",
})
export class FaceDetectionRequestComponent implements OnInit, OnDestroy {
    request: IFaceDetectionRequest;
    results: IDetectionResult[];
    id: number;
    private sub: any;

    constructor(private route: ActivatedRoute, private requestDownloader: FaceDetectionService) {}


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params["id"]; // (+) converts string 'id' to a number

            // In a real app: dispatch action to load the details here.
        });
        this.requestDownloader.getRequest(this.id.toString())
            .subscribe(result => {
                    this.request = result as IFaceDetectionRequest;
                    console.log(this.request);
                },
            error => { console.log(error) });

        if (this.request.completionTime != null) {
            this.requestDownloader.getRequestResults(this.id.toString())
                .subscribe(result => {
                    this.results = result as IDetectionResult[];
                    console.log(this.results);
                });
        }
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}