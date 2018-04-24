
import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/Observable";


@Injectable()
export class FileDownloaderService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    getFileLink(fileName: string): Observable<Object> {
        const params = new HttpParams().set("fileName", fileName);
        return this.httpClient.get(this.baseUrl + "api/Attachments/GetFileLink", { params });
    };

}