import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'attachment-list',
    styleUrls: ['./attachment-list.component.css'],
    templateUrl: './attachment-list.component.html'
})
export class AttachmentListComponent {
    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    upload(event: Event) {
        const inputElement = <HTMLInputElement>(event.srcElement || event.target);
        const files = inputElement.files;

        if (!files) return;

        const formData = new FormData();
        for (var index = 0; index < files.length; index++) {
            const file = files[index];
            formData.set(file.name, file);
        }

        console.log(formData);

        this.httpClient.post<{ contents: string }>(this.baseUrl + 'upload', formData)
            .subscribe(
                response => console.log(response),
                error => console.log(error));

        // Clear the value to allow uploading the same file again.
        inputElement.value = '';
    }
}