import { Component, OnInit } from "@angular/core";
import {FaceDetectionService} from "../../services/face-detection.service";
import {FaceDetectionRequest} from "../../interfaces/face-detection-request";


@Component({
    selector: "app-face-detections",
    templateUrl: "./face-detections.component.html",
    styleUrls: ["./face-detections.component.css"]
})
export class FaceDetectionsComponent implements OnInit {
    requests: FaceDetectionRequest[];

    constructor(private requestDownloader: FaceDetectionService) {}

    ngOnInit() {
        this.requestDownloader.getAllFaceDetectionRequests()
            .subscribe(result => {
                    this.requests = result as FaceDetectionRequest[];
                    console.log(this.requests);
                },
                error => { console.log(error) });
    }

}