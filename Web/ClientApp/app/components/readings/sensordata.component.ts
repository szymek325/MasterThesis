import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'sensordata',
    templateUrl: './sensordata.component.html'
})
export class SensorDataComponent {
    public rooms: Room[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {

        http.get(baseUrl + 'api/SensorDataController/GetRooms').subscribe(result => {
            this.rooms = result.json() as Room[];
        }, error => console.error(error));

    }
}


interface Room {
    id: number;
    name: string;
}
