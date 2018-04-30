import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
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
    formData: any;
    detectionForm: FormGroup;
    file: any;
    isFileValid:boolean;

    constructor(private requestDownloader: FaceDetectionService, private alertService: AlertService, private router: Router, private formBuilder: FormBuilder) {}


    onClickSubmit(data) {

        this.formData = new FormData();
        this.formData.set(this.file.name, this.file);
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

    validateFile(fileInput: any) {
        this.file = fileInput.target.files[0];
        this.isFileValid = this.checkExtension(this.file.name);
        if (this.isFileValid) {

        }

    }

    ngOnInit() {
        this.isFileValid = false;
        this.detectionForm = this.formBuilder.group({
            name: ['name', [Validators.required, Validators.minLength(3)]],
        });
    }

    private checkExtension(name:string) {
        let valToLower = name.toLowerCase();
        let regex = new RegExp("(.*?)\.(jpg|png|jpeg)$");
        let regexTest = regex.test(valToLower);
        //return !regexTest ? { "notSupportedFileType": true } : null;
        return regexTest ;
    }


}