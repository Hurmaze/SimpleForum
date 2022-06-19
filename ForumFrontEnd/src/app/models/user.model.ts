import { withLatestFrom } from "rxjs";

export class User {
    Id: number;
    Email: string;
    Nickname: string;
    ThreadsIds: number[];
    PostsIds: number[];
    constructor(id:number, email: string, nickname: string, threads: number[], posts: number[]){
        this.Id = id;
        this.Email = email,
        this.Nickname = nickname;
        this.ThreadsIds = threads;
        this.PostsIds = posts;
    }
}
