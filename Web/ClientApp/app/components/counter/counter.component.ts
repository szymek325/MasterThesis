import { Component, OnInit } from '@angular/core';
import {FileDownloaderService} from "../../file-downloader.service";

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent {

    constructor(private fileDownloader: FileDownloaderService) {}

    public currentCount = 0;

    public incrementCounter() {
        this.fileDownloader.getFile("stare.PNG");
        this.currentCount++;
    }

}
