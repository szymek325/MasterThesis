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
        this.fileDownloader.getFileLink("1h_realbench.PNG")
            .subscribe(result => {
            this.links = result as FileLink;
                this.imagePath = this.links.url;
                console.log(this.links);
            },
            error => { console.log(error) });


        this.currentCount++;
    }

    ngOnInit() {

    }


}


interface FileLink {
    url: string;
}