import { Component, OnInit } from "@angular/core";
import {Router} from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
    selector: "app-new-person",
    templateUrl: "./new-person.component.html",
    styleUrls: ["./new-person.component.css"]
})
export class NewPersonComponent implements OnInit {
    formData: any;
    personForm: FormGroup;
    files: File[]=[];
    isFileValid: boolean;

    constructor(private router: Router, private formBuilder: FormBuilder) {}

    validateFile(fileInput: any) {
        const input = fileInput.target.files;
        for (let file of input)
            if (file != null) {
                this.isFileValid = this.checkExtension(file.name);

                if (this.isFileValid) {
                    this.files.push(file);
                } else {
                    alert(file.name + " has invalid type");
                }
            }
    }

    ngOnInit() {
        this.isFileValid = false;
        this.personForm = this.formBuilder.group({
            name: ["name", [Validators.required, Validators.minLength(3)]],
        });
    }

    private checkExtension(name: string) {
        const valToLower = name.toLowerCase();
        const regex = new RegExp("(.*?)\.(jpg|png|jpeg)$");
        const regexTest = regex.test(valToLower);
        //return !regexTest ? { "notSupportedFileType": true } : null;
        return regexTest;
    }

}