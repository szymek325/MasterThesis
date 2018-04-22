import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs/Subscription';
import {FileUploaderService} from "../../file-uploader.service";

var inputElement: HTMLInputElement;

@Component({
    selector: 'attachment-list',
    styleUrls: ['./attachment-list.component.css'],
    templateUrl: './attachment-list.component.html'
})
export class AttachmentListComponent {
    files: any;
    formData: any;
    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private fileUploader:FileUploaderService) { }

    add(event: Event) {
        inputElement = <HTMLInputElement>(event.srcElement || event.target);
        this.files = inputElement.files;

        if (!this.files) return;

        this.formData = new FormData();
        for (var index = 0; index < this.files.length; index++) {
            const file = this.files[index];
            this.formData.set(file.name, file);
        }

        console.log(this.formData);
    }

    upload() {
        this.fileUploader.uploadFiles(this.formData)
    }

    clearFiles() {
        inputElement.value = '';
    }
}

interface IFile {
    name: string;
    file: File;
}
