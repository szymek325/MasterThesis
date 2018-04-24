import { Component, OnInit, Inject } from "@angular/core";
import {FileDownloaderService} from "../../services/file-downloader.service";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: "counter",
    templateUrl: "./counter.component.html"
})
export class CounterComponent implements OnInit {

    constructor(private httpClient: HttpClient,
        @Inject("BASE_URL") private baseUrl: string,
        private fileDownloader: FileDownloaderService) {
    }

    imagePath;
    currentCount = 0;

    incrementCounter() {



        this.currentCount++;
    }

    ngOnInit() {

    }


}
