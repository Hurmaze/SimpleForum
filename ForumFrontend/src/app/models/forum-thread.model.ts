export class ForumThread {
    id: number=0;
    themeName: string='';
    title: string='';
    timeCreated: Date = new Date();
    content: string='';
    authorId: number=0;
    authorEmail: string='';
    authorNickname:string='';
    threadPostsIds: number[]=[];
    themeId: number=0;
}
