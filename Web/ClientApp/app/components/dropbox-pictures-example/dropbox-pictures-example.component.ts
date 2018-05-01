import { Component, OnInit } from '@angular/core';
import {FileDownloaderService} from "../../services/file-downloader.service";
import {IFileLink as FileLink} from "../../interfaces/file-link";

@Component({
  selector: 'app-dropbox-pictures-example',
  templateUrl: './dropbox-pictures-example.component.html',
  styleUrls: ['./dropbox-pictures-example.component.css']
})
export class DropboxPicturesExampleComponent implements OnInit {
    links: FileLink;
    imagePath;
    constructor(private fileDownloader: FileDownloaderService) { }

    ngOnInit() {
        this.fileDownloader.getFileLink("1h_realbench.PNG")
            .subscribe(result => {
                this.links = result as FileLink;
                    this.imagePath = this.links.url;
                    console.log(this.links);
                },
                error => { console.log(error) });
  }

}
