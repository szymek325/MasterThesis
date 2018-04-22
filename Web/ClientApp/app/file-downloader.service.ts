import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams, } from "@angular/common/http"
import { Observable } from 'rxjs/Observable';


@Injectable()
export class FileDownloaderService {

    constructor(private httpClient: HttpClient, @Inject("BASE_URL") private baseUrl: string) {}

    //getFile(fileName: string) {

    //    //const params = new HttpParams().set("fileName", fileName);
    //    //this.httpClient.get(this.baseUrl + "getFile", { responseType: "application/octet-stream", params:params})
    //    //    .subscribe(
    //    //        response => {
    //    //            console.log(response);
    //    //        },
    //    //        error => { console.log(error) });
    //}

    getFile(fileName: string): Observable<Blob> {
        const params = new HttpParams().set("fileName", fileName);
        return this.httpClient
            .get(this.baseUrl + "getFile", {
                responseType: "blob",
                params:params
            });
    }

}