import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { PeopleService } from "../../services/people.service";
import {IPerson} from "../../interfaces/person";

@Component({
    selector: "app-people",
    templateUrl: "./people.component.html",
    styleUrls: ["./people.component.css"]
})
export class PeopleComponent implements OnInit {
    people: any;

    constructor(private router: Router, private peopleService: PeopleService) {}

    newPerson() {
        this.router.navigateByUrl("/new-person");
    }

    showPerson(id) {
        this.router.navigateByUrl(`/person/${id}`);
    }

    deletePerson(id) {
        this.peopleService.deletePerson(id).subscribe(result => {
            console.log(result);
                this.ngOnInit();
            },
            error => { console.log(error) });
    }

    ngOnInit() {
        this.peopleService.getAllPeople()
            .subscribe(result => {
                    this.people = result as IPerson[];
                    console.log(this.people);
                },
                error => { console.log(error) });
    }

}