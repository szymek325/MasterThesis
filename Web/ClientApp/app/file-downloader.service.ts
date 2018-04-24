
import { Injectable, Inject, EventEmitter } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/Observable";


@Injectable()
export class FileDownloaderService {
    links: FileLink;


    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    getFile(fileName: string): Observable<Blob> {
        const objectUrl: string = null;
        const params = new HttpParams().set("fileName", fileName);
        return this.httpClient.get(this.baseUrl + "api/Attachments/GetFileLink", { responseType: "blob", params: params });

    }

    getFileNormal(fileName: string) {
        const params = new HttpParams().set("fileName", fileName);
        var cartData = new EventEmitter<any>();
        this.httpClient.get(this.baseUrl + "api/Attachments/GetFileLink", { params }).subscribe(result => {
                this.links = result as FileLink;
                cartData.emit(this.links);
                console.log(this.links);
            },
            error => { console.log(error) });
    }

    getFileLink(fileName: string): Observable<Object> {
        const objectUrl: string = null;
        const params = new HttpParams().set("fileName", fileName);
        return this.httpClient.get(this.baseUrl + "api/Attachments/GetFileLink", { params });
    };

}

interface FileLink {
    url: string;
}