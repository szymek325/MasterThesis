import { Component, OnInit} from "@angular/core";
import {FaceDetectionService} from "../../services/face-detection.service";
import { FaceDetectionRequest } from "../../interfaces/face-detection-request";
import { Router } from "@angular/router";


@Component({
    selector: "app-face-detections",
    templateUrl: "./face-detections.component.html",
})
export class FaceDetectionsComponent implements OnInit {
    requests: FaceDetectionRequest[];

    constructor(private requestDownloader: FaceDetectionService, private router: Router) {}

    newRequest() {
        this.router.navigateByUrl("/new-face-detection");
    }



    ngOnInit() {
        this.requestDownloader.getAllFaceDetectionRequests()
            .subscribe(result => {
                    this.requests = result as FaceDetectionRequest[];
                    console.log(this.requests);
                },
                error => { console.log(error) });
    }

}