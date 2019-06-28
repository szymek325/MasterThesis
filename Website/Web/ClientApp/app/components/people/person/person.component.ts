import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { PeopleService } from "../../../services/people.service";
import { IPerson } from "../../../interfaces/person";

@Component({
    selector: "app-person",
    templateUrl: "./person.component.html",
    styleUrls: ["./person.component.css"]
})
export class PersonComponent implements OnInit {
    person: any;
    id: number;
    private sub: any;

    constructor(private route: ActivatedRoute, private peopleService: PeopleService) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params["id"]; // (+) converts string 'id' to a number

            // In a real app: dispatch action to load the details here.
        });
        this.peopleService.getPerson(this.id.toString())
            .subscribe(result => {
                    this.person = result as IPerson;
                    console.log(this.person);
                },
                error => { console.log(error) });
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }

}