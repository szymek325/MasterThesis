import { Component, OnInit } from "@angular/core";
import {Router, ActivatedRoute} from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AlertService } from "../../../services/alert.service";
import { PeopleService } from "../../../services/people.service";

@Component({
    selector: "app-new-person",
    templateUrl: "./new-person.component.html",
    styleUrls: ["./new-person.component.css"]
})
export class NewPersonComponent implements OnInit {
    id:any;
    formData: any;
    personForm: FormGroup;
    previewFiles: IPreviewFile[]=[];
    isFileValid: boolean;
    private sub: any;

    constructor(private route: ActivatedRoute, private peopleService:PeopleService,private router: Router, private formBuilder: FormBuilder, private alertService: AlertService) {}

    validateFile(fileInput: any) {
        this.alertService.clear();
        const input = fileInput.target.files;
        for (let file of input)
            if (file != null) {
                this.isFileValid = this.checkExtension(file.name);
                if (this.isFileValid) {
                    if (!this.previewFiles.some(x => x.file.name === file.name)) {
                        const reader = new FileReader();
                        reader.onload = (fileInput: any) => {
                            var previewFile: IPreviewFile = {
                                url: fileInput.target.result,
                                file: file
                            };
                            this.previewFiles.push(previewFile);
                            console.log(this.previewFiles);
                        };
                        reader.readAsDataURL(file);
                    } else {
                        this.alertService.error(file.name + " is already on the list");
                    }
                } else {
                    this.alertService.error(file.name + " has invalid type. File must be of type .jpeg, .jpg or .png");
                }
            }
    }

    onClickSubmit(data) {

        this.formData = new FormData();
        for (let file of this.previewFiles) {
            this.formData.set(file.file.name, file.file);
        }
        this.formData.set("name", data.name);
        console.log(this.formData);

        this.peopleService.createNewPerson(this.formData)
            .subscribe(result => {
                    if (result === 0) {
                        alert("Exception occured during request creation");
                    }
                    this.router.navigateByUrl("people");
                },
                error => {
                    console.log(error.message);
                    this.alertService.error(error.message);
                });
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

export interface IPreviewFile {
    url: string;
    file: File;
};