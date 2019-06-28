import { Component, OnInit } from "@angular/core";
import { FaceRecognitionService } from "../../services/face-recognition.service";
import { Router } from "@angular/router";
import {IFaceRecognition} from"../../interfaces/recognition/face-recognition";

@Component({
    selector: "app-face-recognitions",
    templateUrl: "./face-recognitions.component.html",
    styleUrls: ["./face-recognitions.component.css"]
})
export class FaceRecognitionsComponent implements OnInit {
    requests: IFaceRecognition[];

    constructor(private requestDownloader: FaceRecognitionService, private router: Router) {}

    newRequest() {
        this.router.navigateByUrl("/new-face-recognition");
    }

    showRequest(id) {
        this.router.navigateByUrl(`/face-recognition/${id}`);
    }


    ngOnInit() {
        this.requestDownloader.getAllFaceRecognitions()
            .subscribe(result => {
                    this.requests = result as IFaceRecognition[];
                    console.log(this.requests);
                },
                error => { console.log(error) });
    }

}