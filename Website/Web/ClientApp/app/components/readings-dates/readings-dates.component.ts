import { Component, OnInit } from '@angular/core';
import { ReadingsProviderService } from "../../services/readings-provider.service";
import { Router } from "@angular/router";
import { IDate } from "../../interfaces/date";

@Component({
    selector: 'app-readings-dates',
    templateUrl: './readings-dates.component.html',
    styleUrls: ['./readings-dates.component.css']
})
export class ReadingsDatesComponent implements OnInit {
    days: IDate[];

    constructor(private router: Router, private readingsProvider: ReadingsProviderService) {}

    showReadings(day) {
        this.router.navigateByUrl(`/sensor-readings/${day}`);
    }

    ngOnInit() {
        this.readingsProvider.getAllDates()
            .subscribe(result => {
                    this.days = result as IDate[];
                    console.log(this.days);
                },
                error => { console.log(error) });
    }

}