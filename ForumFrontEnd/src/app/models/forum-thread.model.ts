export class ForumThread {
    Id: number;
    ThemeName: string;
    Title: string;
    TimeCreated: Date;
    Content: string;
    AuthorId: number;
    ThreadPostsIds: number[];
    constructor(id: number, themename: string, title: string, time: Date, content: string, authorid: number, threadposts: number[]){
        this.Id = id;
        this.ThemeName = themename;
        this.Title = title;
        this.TimeCreated = time;
        this.Content = content;
        this.AuthorId = authorid;
        this.ThreadPostsIds = threadposts;
    }
}
