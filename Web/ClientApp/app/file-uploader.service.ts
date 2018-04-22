import { Injectable,Inject } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable()
export class FileUploaderService {

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  uploadFiles(formData: string){
      this.httpClient.post<{ contents: string }>(this.baseUrl + 'upload', formData)
          .subscribe(
              response => {
                  console.log(response);
              },
              error => console.log(error));
  }

}
