import { Component, OnInit } from "@angular/core";
import {FaceDetectionService} from "../../services/face-detection.service";
import {AlertService} from "../../services/alert.service";

var inputElement: HTMLInputElement;

@Component({
    selector: "new-face-detection",
    templateUrl: "./new-face-detection.component.html",
    styleUrls: ["./face-detection.components.css"],
})
export class NewFaceDetectionComponent implements OnInit {
    name: string;
    files: any;
    formData: any;

    constructor(private requestDownloader: FaceDetectionService, private alertService: AlertService) { }

    add(event: Event) {
        inputElement = ((event.srcElement || event.target) as HTMLInputElement);
        this.files = inputElement.files;

        if (!this.files) return;

        this.formData = new FormData();
        for (let index = 0; index < this.files.length; index++) {
            const file = this.files[index];
            this.formData.set(file.name, file);
        }

        console.log(this.formData);
    }

    ngOnInit() {

    }



}