import { Component, OnInit, ViewChild } from "@angular/core";
import {FaceDetectionService} from "../../services/face-detection.service";
import {AlertService} from "../../services/alert.service";
import { Router } from "@angular/router";
import { FormGroup, FormControl, Validators, FormBuilder } from "@angular/forms";

@Component({
    selector: "new-face-detection",
    templateUrl: "./new-face-detection.component.html",
    styleUrls: ["./face-detection.components.css"],
})
export class NewFaceDetectionComponent implements OnInit {
    faceDetectionForm;
    name: string;
    files: any;
    faceImage: any;
    formData: any;
    @ViewChild("FaceImage")
    Face_Image;

    constructor(private requestDownloader: FaceDetectionService, private alertService: AlertService, private router: Router, private formBuilder: FormBuilder) {}


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
                error => {
                    console.log(error.message);
                    this.alertService.error(error.message);
                });
    }

    validateFile() {
        const Image = this.Face_Image.nativeElement;
        if (Image.files && Image.files[0]) {
            this.faceImage = Image.files[0];
            console.log(this.faceImage);
        }
    }

    ngOnInit() {
        this.detectionForm = this.formBuilder.group({
            name: ['name', [Validators.required, Validators.minLength(3)]],
            file:[]
        });
    }


    detectionForm: FormGroup;
}