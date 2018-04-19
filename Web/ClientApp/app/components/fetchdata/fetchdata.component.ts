import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    forecasts!: WeatherForecast[];
    rooms!: Room[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
            this.forecasts = result as WeatherForecast[];
        }, error => console.error(error));

        http.get(baseUrl + 'api/SensorDataController/GetRooms').subscribe(result => {
            this.rooms = result as Room[];
        }, error => console.error(error));

    }

}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

interface Room {
    id: number;
    name: string;
}
