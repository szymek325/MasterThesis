import { Component } from "@angular/core";
import {FileUploaderService} from "../../services/file-uploader.service";
import {AlertService} from "../../services/alert.service";

var inputElement: HTMLInputElement;

@Component({
    selector: "attachment-list",
    styleUrls: ["./attachment-list.component.css"],
    templateUrl: "./attachment-list.component.html"
})
export class AttachmentListComponent {
    files: any;
    formData: any;

    constructor(private fileUploader: FileUploaderService, private alertService: AlertService) {
    }

    add(event: Event) {
        inputElement = ((event.srcElement || event.target) as HTMLInputElement);
        this.files = inputElement.files;
        console.log(this.files);
        if (!this.files) return;

        this.formData = new FormData();
        for (let index = 0; index < this.files.length; index++) {
            const file = this.files[index];
            this.formData.set(file.name, file);
        }

        console.log(this.formData);
    }

    upload() {
        this.fileUploader.uploadFiles(this.formData)
            .subscribe(
                response => {
                    console.log(response);
                    this.clearFiles();
                    this.files = [];
                },
                error => {
                    console.log(error);
                    this.alertService.error("dupa");
                });
    }

    clearFiles() {
        this.formData = new FormData();
    }
}

interface IFile {
    name: string;
    file: File;
}