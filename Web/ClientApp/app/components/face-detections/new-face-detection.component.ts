import { Component, OnInit, ViewChild } from "@angular/core";
import {FaceDetectionService} from "../../services/face-detection.service";
import {AlertService} from "../../services/alert.service";
import { Router } from "@angular/router";

@Component({
    selector: "new-face-detection",
    templateUrl: "./new-face-detection.component.html",
    styleUrls: ["./face-detection.components.css"],
})
export class NewFaceDetectionComponent implements OnInit {
    name: string;
    files: any;
    faceImage: any;
    formData: any;
    @ViewChild("FaceImage")
    Face_Image;

    constructor(private requestDownloader: FaceDetectionService, private alertService: AlertService, private router: Router) {}


    onClickSubmit(data) {
        const Image = this.Face_Image.nativeElement;
        if (Image.files && Image.files[0]) {
            this.faceImage = Image.files[0];
            console.log(this.faceImage);
        }
        this.formData = new FormData();
        this.formData.set(this.faceImage.name, this.faceImage);
        this.formData.set("name", data.name);
        console.log(this.formData);

        this.requestDownloader.createNewRequest(this.formData)
            .subscribe(result => {
                    this.router.navigateByUrl("face-detection");
                },
                error => { console.log(error) });
    }

    ngOnInit() {

    }


}