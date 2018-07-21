import { IFileLink } from "../file-link";
import { IDetectionResult } from "./detection-result";

export interface IFaceDetectionRequest{
    id: number;
    name: string;
    status: string;
    dnn: number;
    haar: number;
    thumbnail: string;
    fileLink: IFileLink;
    creationTime: string;
    completionTime: string;
    results: IDetectionResult[];
}