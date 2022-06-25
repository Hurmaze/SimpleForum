export class Theme {
    id: number;
    themeName: string;
    threadsIds: number[];
    constructor(id:number, themename: string, threads: number[]){
        this.id = id;
        this.themeName = themename,
        this.threadsIds = threads;
    }
}
