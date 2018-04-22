import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import {FileDownloaderService} from "../../file-downloader.service";
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent implements OnInit {

    @ViewChild("heroImage") image: ElementRef;

    constructor(private fileDownloader: FileDownloaderService) {}

    public currentCount = 0;

    public incrementCounter() {

        this.fileDownloader.getFileNormal("1h_realbench.PNG");

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


