import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'sensordata',
    templateUrl: './sensordata.component.html'
})
export class SensorDataComponent {
    rooms!: Room[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

        http.get(baseUrl + 'api/SensorDataController/GetRooms').subscribe(result => {
            this.rooms = result as Room[];
        }, error => console.error(error));

    }
}


interface Room {
    id: number;
    name: string;
}
