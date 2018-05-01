import { Component, OnInit} from "@angular/core";
import {FaceDetectionService} from "../../services/face-detection.service";
import { IFaceDetectionRequest} from "../../interfaces/face-detection-request";
import { Router } from "@angular/router";


@Component({
    selector: "app-face-detections",
    templateUrl: "./face-detections.component.html",
})
export class FaceDetectionsComponent implements OnInit {
    requests: IFaceDetectionRequest[];

    constructor(private requestDownloader: FaceDetectionService, private router: Router) {}

    newRequest() {
        this.router.navigateByUrl("/new-face-detection");
    }

    showRequest(id) {
        this.router.navigateByUrl(`/face-detection-request/${id}`);
    }


    ngOnInit() {
        this.requestDownloader.getAllFaceDetectionRequests()
            .subscribe(result => {
                    this.requests = result as IFaceDetectionRequest[];
                    console.log(this.requests);
                },
                error => { console.log(error) });
    }

}