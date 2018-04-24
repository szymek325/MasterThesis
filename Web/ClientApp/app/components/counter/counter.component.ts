import { Component, OnInit, Inject } from "@angular/core";
import {FileDownloaderService} from "../../file-downloader.service";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "counter",
    templateUrl: "./counter.component.html"
})
export class CounterComponent implements OnInit {
    links: IFileLink;

    constructor(private httpClient: HttpClient,
        @Inject("BASE_URL") private baseUrl: string,
        private fileDownloader: FileDownloaderService) {
    }

    imagePath;
    currentCount = 0;

    incrementCounter() {
        this.fileDownloader.getFileLink("1h_realbench.PNG")
            .subscribe(result => {
                    this.links = result as IFileLink;
                    this.imagePath = this.links.url;
                    console.log(this.links);
                },
                error => { console.log(error) });


        this.currentCount++;
    }

    ngOnInit() {

    }


}


interface IFileLink {
    url: string;
}