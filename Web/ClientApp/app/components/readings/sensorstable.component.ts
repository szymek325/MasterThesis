import {Component, Inject} from '@angular/core';
import {Http} from "@angular/http";


@Component({
    selector: 'sensortable',
    template: `
                <p *ngIf="!sensors"><em>Sensors table is being loaded...</em></p>
                
                <table class='table' *ngIf="sensors">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Room Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr *ngFor="let sensor of sensors">
                            <td>{{ sensor.sensorId }}</td>
                            <td>{{ sensor.sensorName }}</td>
                            <td>{{ sensor.roomName }}</td>
                        </tr>
                    </tbody>
                </table>
               `,
    styleUrls: ['./sensordata.component.css']
})
export class SensorTableComponent {
    public sensors: Sensor[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SensorDataController/GetSensors').subscribe(result => {
            this.sensors = result.json() as Sensor[];
        }, error => console.error(error));
    }
}


interface Sensor {
    id: number;
    sensorName: string;
    roomName: string;
}