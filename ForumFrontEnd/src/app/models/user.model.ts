export class User {
    id: number;
    email: string;
    nickname: string;
    threadsIds: number[];
    postsIds: number[];
    constructor(id:number, email: string, nickname: string, threads: number[], posts: number[]){
        this.id = id;
        this.email = email,
        this.nickname = nickname;
        this.threadsIds = threads;
        this.postsIds = posts;
    }
}
