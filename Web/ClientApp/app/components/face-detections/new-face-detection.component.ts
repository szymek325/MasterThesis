import { Component, OnInit } from "@angular/core";
import {FaceDetectionService} from "../../services/face-detection.service";
import {AlertService} from "../../services/alert.service";


@Component({
    selector: "new-face-detection",
    templateUrl: "./new-face-detection.component.html",
})
export class NewFaceDetectionComponent implements OnInit {
    name: string;
    files:any;
    constructor(private requestDownloader: FaceDetectionService, private alertService: AlertService) {}

    ngOnInit() {

    }

}