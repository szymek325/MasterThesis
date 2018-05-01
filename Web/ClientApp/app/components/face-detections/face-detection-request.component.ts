import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { FaceDetectionService } from "../../services/face-detection.service";
import {IFaceDetectionRequest} from "../../interfaces/face-detection-request";

@Component({
    selector: "face-detection-request",
    templateUrl: "./face-detection-request.component.html",
})
export class FaceDetectionRequestComponent implements OnInit, OnDestroy {
    request: IFaceDetectionRequest;
    haarLink;
    dnnLink;
    inputLink;
    id: number;
    private sub: any;

    constructor(private route: ActivatedRoute, private requestDownloader: FaceDetectionService) {}


    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params["id"]; // (+) converts string 'id' to a number

            // In a real app: dispatch action to load the details here.
        });
        this.requestDownloader.getRequest(this.id.toString())
            .subscribe(result => {
                    this.request = result as IFaceDetectionRequest;
                    this.dnnLink = this.request.fileLinks.filter(x => x.fileName.split(".")[0] === "dnn")[0];
                    this.haarLink = this.request.fileLinks.filter(x => x.fileName.split(".")[0] === "haar")[0];
                    this.inputLink = this.request.fileLinks.filter(x => x.fileName.split(".")[0] === "input")[0];
                    console.log(this.request);
                },
                error => { console.log(error) });
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}