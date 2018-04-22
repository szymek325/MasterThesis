import { Component, OnInit, Inject } from "@angular/core";
import {FileDownloaderService} from "../../file-downloader.service";
import { HttpClient, HttpParams } from "@angular/common/http";

@Component({
    selector: "counter",
    templateUrl: "./counter.component.html"
})
export class CounterComponent implements OnInit {
    links: FileLink;

    constructor(private httpClient: HttpClient,
        @Inject("BASE_URL") private baseUrl: string,
        private fileDownloader: FileDownloaderService) {
    }

    imagePath;
    currentCount = 0;

    incrementCounter() {
        const params = new HttpParams().set("fileName", "1h_realbench.PNG");
        this.httpClient.get(this.baseUrl + "getFileLink", { params }).subscribe(result => {
            this.links = result as FileLink;
                this.imagePath = this.links.url;
                console.log(this.links);
            },
            error => { console.log(error) });


        //const data = this.fileDownloader.getFileNormal("1h_realbench.PNG");
        //console.log("data in counter");
        //console.log(data);
        //this.imagePath = data.url;
        this.currentCount++;
    }

    ngOnInit() {

        //this.fileDownloader.getFileNormal("stare.PNG").subscribe(
        //    m => {
        //        objectUrl = URL.createObjectURL(m);
        //        console.log(objectUrl);
        //    },
        //    error => { console.log(error) });
    }


}


interface FileLink {
    url: string;
}