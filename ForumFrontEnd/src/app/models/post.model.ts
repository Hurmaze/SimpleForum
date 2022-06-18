export class Post {
    Id: number;
    AuthorId: number;
    ThreadId: number;
    Content: string;
    TimeCreated: Date;
    constructor(id: number, authorid: number, threadid: number, content: string, time: Date){
        this.Id = id;
        this.AuthorId = authorid;
        this.ThreadId = threadid;
        this.Content = content;
        this.TimeCreated = time;
    }
}
