import { IFileLink } from "./file-link";

export interface IPerson {
    id: number;
    name: string;
    thumbnail: string;
    fileLinks: IFileLink[];
    checked: false;
}