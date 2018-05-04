import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: "app-people",
    templateUrl: "./people.component.html",
    styleUrls: ["./people.component.css"]
})
export class PeopleComponent implements OnInit {

    constructor(private router: Router) {}

    newPerson() {
        this.router.navigateByUrl("/new-person");
    }

    ngOnInit() {
    }

}