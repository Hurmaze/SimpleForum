export class Post {
    id: number=0;
    authorId: number=0;
    threadId: number=0;
    authorEmail: string='';
    authorNickname:string='';
    content: string='';
    timeCreated: Date=new Date();
}
