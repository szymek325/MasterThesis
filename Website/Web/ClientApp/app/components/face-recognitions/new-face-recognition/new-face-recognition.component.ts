import { Component, OnInit } from "@angular/core";
import {FormGroup, FormBuilder, Validators} from "@angular/forms";
import { FaceRecognitionService } from "../../../services/face-recognition.service";
import { AlertService } from "../../../services/alert.service";
import { Router } from "@angular/router";
import { NeuralNetworksService } from "../../../services/neural-networks.service";
import { INnBaseInfo } from "../../../interfaces/neuralNetwork/nn-base-info";

@Component({
    selector: "app-new-face-recognition",
    templateUrl: "./new-face-recognition.component.html",
    styleUrls: ["./new-face-recognition.component.css"]
})
export class NewFaceRecognitionComponent implements OnInit {
    neuralNetworks: INnBaseInfo[];
    formData: any;
    recognitionForm: FormGroup;
    file: any;
    isFileValid: boolean;

    constructor(private requestDownloader: FaceRecognitionService,
        private nnService: NeuralNetworksService,
        private alertService: AlertService,
        private router: Router,
        private formBuilder: FormBuilder) {
    }

    onClickSubmit(data) {
        console.log(data);
        console.log(this.recognitionForm.controls["neuralOption"].value);
        this.formData = new FormData();
        this.formData.set("name", data.name);
        this.formData.set("neuralNetworkId", data.neuralOption);
        this.formData.set(this.file.name, this.file);
        console.log(this.formData);

        this.requestDownloader.createRequest(this.formData)
            .subscribe(result => {
                    if (result === 0) {
                        alert("Exception occured during request creation");
                    }
                    console.log(result);
                    this.router.navigateByUrl("face-recognitions");
                },
                error => {
                    console.log(error.message);
                    this.alertService.error(error.message);
                });
    }

    validateFile(fileInput: any) {
        this.file = fileInput.target.files[0];
        if (this.file != null) {
            this.isFileValid = this.checkExtension(this.file.name);
        } else {
            this.isFileValid = false;
        }
    }

    ngOnInit() {
        this.isFileValid = false;
        this.recognitionForm = this.formBuilder.group({
            name: ["name", [Validators.required, Validators.minLength(3)]],
            neuralOption: ["", [Validators.required]],
        });
        this.nnService.getAllCompletedNeuralNetworks()
            .subscribe(result => {
                    this.neuralNetworks = result as INnBaseInfo[];
                    console.log(this.neuralNetworks);
                },
                error => { console.log(error) });
    }

    private checkExtension(name: string) {
        const valToLower = name.toLowerCase();
        const regex = new RegExp("(.*?)\.(jpg|png|jpeg)$");
        const regexTest = regex.test(valToLower);
        //return !regexTest ? { "notSupportedFileType": true } : null;
        return regexTest;
    }

}