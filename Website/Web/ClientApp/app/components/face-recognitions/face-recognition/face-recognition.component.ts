import { Component, OnInit } from "@angular/core";
import { FaceRecognitionService } from "../../../services/face-recognition.service";
import { ActivatedRoute } from "@angular/router";
import { IFaceRecognition } from "../../../interfaces/recognition/face-recognition";

@Component({
    selector: "app-face-recognition",
    templateUrl: "./face-recognition.component.html",
    styleUrls: ["./face-recognition.component.css"]
})
export class FaceRecognitionComponent implements OnInit {
    request: any;
    id: number;
    private sub: any;

    constructor(private route: ActivatedRoute, private faceRecognitionService: FaceRecognitionService) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params["id"]; // (+) converts string 'id' to a number

            // In a real app: dispatch action to load the details here.
        });
        this.faceRecognitionService.getFaceRecognition(this.id.toString())
            .subscribe(result => {
                    this.request = result as IFaceRecognition;
                    console.log(this.request);
                },
                error => { console.log(error) });
    }

}