import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";


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
    sensors!: Sensor[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SensorDataController/GetSensors').subscribe(result => {
            this.sensors = result as Sensor[];
        }, error => console.error(error));
    }
}


interface Sensor {
    id: number;
    sensorName: string;
    roomName: string;
}