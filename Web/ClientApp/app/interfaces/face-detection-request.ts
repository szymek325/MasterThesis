import {IFileLink as FileLink} from "./file-link";

export interface IFaceDetectionRequest{
    id: number;
    name: string;
    status: string;
    dnn: number;
    haar: number;
    fileLinks:
    {
        [key:string]:FileLink
    };
}