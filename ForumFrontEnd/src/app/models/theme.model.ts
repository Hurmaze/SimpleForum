export class Theme {
    Id: number;
    ThemeName: string;
    ThreadsIds: number[];
    constructor(id:number, themename: string, threads: number[]){
        this.Id = id;
        this.ThemeName = themename,
        this.ThreadsIds = threads;
    }
}
