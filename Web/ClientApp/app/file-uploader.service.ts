import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http"
import { Observable } from "rxjs/Observable";

@Injectable()
export class FileUploaderService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    uploadFiles(formData: string): Observable<Object> {
        return this.httpClient.post<{ contents: string }>(this.baseUrl + "api/Attachments/UploadAsync", formData);
    }

}